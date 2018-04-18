using System;

using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    public sealed partial class ResourceSyncDocument
    {
        [PublicAPI]
        public static class Factory
        {
            /// <summary>
            /// when building a capabilitylist, each location is required to be marked with metadata for one
            /// of the known capabilities
            /// </summary>
            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateCapabilityList(Uri baseUri, ResourceSet resourceSet, params CapabilityLocation[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.CapabilityList };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(baseUri, UriPartNames.WellKnownResourceSync));
                return sitemap;
            }

            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateChangeDump(Uri baseUri,
                                                                Uri parentCapabilityListUri,
                                                                ResourceSet resourceSet,
                                                                DateTime from,
                                                                params Location[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.ChangeDump, From = from };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(baseUri, parentCapabilityListUri.ToString()));
                return sitemap;
            }

            /// <summary>
            /// when building a change list, each location is required to be marked with metadata with
            /// - loc (resource url)
            /// - lastmod (when the change occurred)
            /// - change (the change type: 'created', 'updated', 'deleted'
            /// </summary>
            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateChangeList(Uri baseUri,
                                                                Uri parentCapabilityListUri,
                                                                ResourceSet resourceSet,
                                                                DateTime from,
                                                                params ChangeLocation[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.ChangeList, From = from };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(parentCapabilityListUri.ToString()));
                return sitemap;
            }

            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateChangeNotification(Uri baseUri, Uri parentUri, ResourceSet resourceSet, params ChangeLocation[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.ChangelistNotification };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(parentUri.ToString()));
                return sitemap;
            }

            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateFrameworkNotification(Uri baseUri, Uri parentUri, ResourceSet resourceSet, params ChangeLocation[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.FrameworkNotification };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(parentUri.ToString()));
                return sitemap;
            }

            [PublicAPI]
            public static ResourceSyncDocument CreateResourceDump(Uri baseUri,
                                                                  Uri parentCapabilityListUri,
                                                                  ResourceSet resourceSet,
                                                                  DateTime at,
                                                                  params Location[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.ResourceDump, At = at };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(parentCapabilityListUri.ToString()));
                return sitemap;
            }

            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateResourceList(Uri baseUri,
                                                                  Uri parentCapabilityListUri,
                                                                  ResourceSet resourceSet,
                                                                  DateTime at,
                                                                  params Location[] operationLocations)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.ResourceList, At = at };
                var sitemap = new ResourceSyncDocument(baseUri, metadata);
                foreach (var operationLocation in operationLocations)
                {
                    sitemap.Add(operationLocation);
                }
                sitemap.Add(Link.CreateUpLink(baseUri, parentCapabilityListUri.ToString()));
                return sitemap;
            }

            [Pure]
            [PublicAPI]
            public static ResourceSyncDocument CreateSourceDescription(Uri baseUri, params Location[] capabilityLists)
            {
                var metadata = new ResourceSyncMetadata { Capability = CapabilityNames.SourceDescription };
                var sitemap = new ResourceSyncDocument(baseUri ?? new Uri("/", UriKind.Relative), metadata);
                foreach (var resourceSetCapability in capabilityLists)
                {
                    sitemap.Add(resourceSetCapability);
                }
                return sitemap;
            }
        }
    }
}