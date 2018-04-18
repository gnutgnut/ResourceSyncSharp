using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

using ResourceSyncSharp.Codecs;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace ResourceSyncSharp.Models
{
    /// <summary>
    /// model of a ResourceSync document, which is itself just a SiteMap document with some small extensions
    ///  - renders to and from json or xml
    ///  - the xml is conformant with the spec
    /// (the json is not normative, but our own transport based on the json.net serialiser)
    /// 
    /// the ResourceSync spec is here:
    /// http://www.niso.org/apps/group_public/download.php/12904/z39-99-2014_resourcesync.pdf
    /// 
    /// the SiteMap spec is here:
    /// http://www.sitemaps.org/protocol.html
    /// 
    /// the W3C datetime format used in SiteMap is here:
    /// http://www.w3.org/TR/NOTE-datetime
    /// </summary>
    [PublicAPI]
    [Serializable]
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public sealed partial class ResourceSyncDocument : IEquatable<ResourceSyncDocument>
    {
        // http://www.niso.org/apps/group_public/download.php/12904/z39-99-2014_resourcesync.pdf
        //   => http://www.sitemaps.org/protocol.html
        //     => http://www.w3.org/TR/NOTE-datetime
        internal const string W3CDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";
        private const string ResourceSyncNamespace = "http://www.openarchives.org/rs/terms";
        private const string ResourceSyncNamespaceAlias = "rs";

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        [NonSerialized]
        [PublicAPI]
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns;

        [JsonIgnore]
        [NonSerialized]
        [XmlIgnore]
        private readonly XmlCodec _xmlCodec;

        private Uri _baseUri;
        private List<Link> _links;
        private List<Location> _map;

        [PublicAPI]
        public ResourceSyncDocument(Uri baseUri, ResourceSyncMetadata metadata) : this()
        {
            _xmlCodec = new XmlCodec(this);
            Debug.Assert(baseUri == null || baseUri.IsAbsoluteUri);
            _baseUri = baseUri;
            Metadata = metadata;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ResourceSyncDocument()
        {
            _xmlCodec = new XmlCodec(this);
            _map = new List<Location>();
            _links = new List<Link>();
            Xmlns = new XmlSerializerNamespaces();
            Xmlns.Add(ResourceSyncNamespaceAlias, ResourceSyncNamespace);
        }

        [JsonIgnore]
        [PublicAPI]
        [XmlIgnore]
        public OperationCapabilities Capability
        {
            [PublicAPI]
            [Pure]
            get
            {
                return (OperationCapabilities)Enum.Parse(typeof(OperationCapabilities), Metadata.Capability);
            }
        }

        [JsonIgnore]
        [PublicAPI]
        [XmlIgnore]
        public XmlCodec XmlCodec
        {
            [PublicAPI]
            [Pure]
            get
            {
                return _xmlCodec;
            }
        }

        [JsonProperty("link")]
        [PublicAPI]
        [XmlElement("ln", Namespace = "http://www.openarchives.org/rs/terms", Order = 2)]
        public Link[] Links
        {
            [PublicAPI]
            [Pure]
            get
            {
                return _links.ToArray();
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                _links.Clear();
                value.ToList().ForEach(_links.Add);
            }
        }

        [JsonProperty("url")]
        [PublicAPI]
        [XmlElement("url", Order = 3)]
        public Location[] Locations
        {
            [PublicAPI]
            [Pure]
            get
            {
                return _map.Select(RebaseLocation).ToArray();
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                var items = value;
                _map.Clear();
                items.ToList().ForEach(_map.Add);
            }
        }

        [JsonProperty("metadata")]
        [PublicAPI]
        [XmlElement("md", Namespace = "http://www.openarchives.org/rs/terms", Order = 1)]
        public ResourceSyncMetadata Metadata
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [JsonIgnore]
        [PublicAPI]
        [XmlIgnore]
        internal Uri BaseUri
        {
            [PublicAPI]
            [Pure]
            get
            {
                return _baseUri;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        internal Location[] RawLocations
        {
            [PublicAPI]
            [Pure]
            get
            {
                return _map.ToArray();
            }
        }

        [PublicAPI]
        [Pure]
        public static ResourceSyncDocument FromJson(JsonString json)
        {
            return JsonCodec.FromJson<ResourceSyncDocument>(json);
        }

        [PublicAPI]
        public void Add(Location item)
        {
            _map.Add(item);
        }

        [PublicAPI]
        public void Add(Link link)
        {
            _links.Add(link);
        }

        [PublicAPI]
        [Pure]
        public bool Is(OperationCapabilities capability)
        {
            return Capability == capability;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Pure]
        public bool ShouldSerializeLinks()
        {
            return Links.Any();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Pure]
        public bool ShouldSerializeLocations()
        {
            return Locations.Any();
        }

        [PublicAPI]
        [Pure]
        public JsonString ToJson()
        {
            return JsonCodec.ToJson(this);
        }

        [Pure]
        private Location RebaseLocation(Location location)
        {
            var rebasedLocation = new Location
            {
                ChangeFrequency = location.ChangeFrequency,
                LastModified = location.LastModified,
                Priority = location.Priority,
                Metadata = location.Metadata,
                Url = _baseUri == null ? location.Url : RebaseUrl(location.Url)
            };
            return rebasedLocation;
        }

        [Pure]
        private Uri RebaseUrl(Uri uri)
        {
            return _baseUri == null ? uri : new Uri(_baseUri, uri);
        }

        [PublicAPI]
        public struct RequestDescriptor
        {
            [PublicAPI]
            public readonly OperationCapabilities Capability;

            [PublicAPI]
            public readonly Uri Location;

            [PublicAPI]
            public readonly SyncScopes SyncScope;

            [PublicAPI]
            public RequestDescriptor(Uri location, OperationCapabilities capability, SyncScopes syncScope)
            {
                Location = location;
                Capability = capability;
                SyncScope = syncScope;
            }
        }

        [PublicAPI]
        public static class CapabilityNames
        {
            [PublicAPI]
            public const string CapabilityList = "capabilitylist";

            [PublicAPI]
            public const string ChangeDump = "changedump";

            [PublicAPI]
            public const string ChangeList = "changelist";

            [PublicAPI]
            public const string ChangelistNotification = "changelist-notification";

            [PublicAPI]
            public const string FrameworkNotification = "framework-notification";

            [PublicAPI]
            public const string ResourceDump = "resourcedump";

            [PublicAPI]
            public const string ResourceList = "resourcelist";

            [PublicAPI]
            public const string SourceDescription = "description";
        }

        [PublicAPI]
        public static class UriBuilder
        {
            [PublicAPI]
            public static string MakeCapabilityRelative(string resourceSet, string capability)
            {
                return string.Join("/", UriPartNames.ResourceSets, resourceSet, capability);
            }

            public static string MakeResourceSetRelative(string resourceSet)
            {
                return string.Join("/", UriPartNames.ResourceSets, resourceSet);
            }
        }

        [PublicAPI]
        public static class UriPartNames
        {
            [PublicAPI]
            public const string ResourceList = "resourcelist";

            [PublicAPI]
            public const string Resources = "resources";

            [PublicAPI]
            public const string ResourceSets = "resourcesets";

            [PublicAPI]
            public const string WellKnownResourceSync = "/.well-known/resourcesync";
        }

        [Pure]
        [PublicAPI]
        public bool Equals(ResourceSyncDocument p)
        {
            if (ReferenceEquals(p, null))
            {
                return false;
            }
            if (ReferenceEquals(this, p))
            {
                return true;
            }
            if (GetType() != p.GetType())
            {
                return false;
            }
            if (!StructuralComparisons.StructuralEqualityComparer.Equals(RawLocations, p.RawLocations))
            {
                return false;
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!StructuralComparisons.StructuralEqualityComparer.Equals(Links, p.Links))
            {
                return false;
            }
            return true;
        }
    }
}