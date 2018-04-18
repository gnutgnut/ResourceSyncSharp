using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

using JetBrains.Annotations;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ResourceSyncSharp.Models
{
    [Serializable]
    public class Location : IStructuralEquatable
    {
        [PublicAPI]
        [Pure]
        public static Location Create(Uri loc)
        {
            return new Location { Url = loc };
        }

        [PublicAPI]
        [Pure]
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                var hashCode = ChangeFrequency.GetHashCode();
                hashCode = (hashCode * 397) ^ LastModified.GetHashCode();
                hashCode = (hashCode * 397) ^ Priority.GetHashCode();
                hashCode = (hashCode * 397) ^ (Url?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Metadata?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        [PublicAPI]
        [Pure]
        protected bool Equals(Location other)
        {
            return ChangeFrequency == other.ChangeFrequency && LastModified.Equals(other.LastModified) && Priority.Equals(other.Priority) && Equals(Url, other.Url)
                   && Equals(Metadata, other.Metadata);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [PublicAPI]
        public enum ChangeFrequencies
        {
            // ReSharper disable InconsistentNaming
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
            // ReSharper enable InconsistentNaming
        }

        [XmlElement("changefreq", Order = 3)]
        [JsonProperty("changefreq")]
        [PublicAPI]
        public ChangeFrequencies? ChangeFrequency
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [XmlIgnore]
        [JsonProperty("lastmod")]
        [PublicAPI]
        public DateTime? LastModified
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        /// <summary>shim for xml serialize of nullable datetime</summary>
        [XmlElement("lastmod", Order = 2)]
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        public string LastModifiedString
        {
            [PublicAPI]
            [Pure]
            get
            {
                return LastModified?.ToString(ResourceSyncDocument.W3CDateTimeFormat) ?? String.Empty;
            }
            set
            {
                LastModified = !string.IsNullOrEmpty(value)
                                   ? DateTime.ParseExact(value, ResourceSyncDocument.W3CDateTimeFormat, CultureInfo.InvariantCulture)
                                   : (DateTime?)null;
            }
        }

        [XmlElement("priority", Order = 4)]
        [JsonProperty("priority")]
        [PublicAPI]
        public double? Priority
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [XmlElement("loc", Order = 1)]
        [JsonProperty("loc")]
        [PublicAPI]
        public string UrlString
        {
            [PublicAPI]
            [Pure]
            get
            {
                return Url.ToString();
            }
            set { Url = new Uri(value, value[0] == '/' ? UriKind.Relative : UriKind.Absolute); }
        }

        [XmlIgnore]
        [JsonIgnore]
        [PublicAPI]
        public Uri Url
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [XmlElement("md", Namespace = "http://www.openarchives.org/rs/terms", Order = 5)]
        [JsonProperty("metadata")]
        [PublicAPI]
        public ResourceSyncMetadata Metadata
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        [Pure]
        public bool ShouldSerializeMetadata()
        {
            return Metadata != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        [Pure]
        public bool ShouldSerializeChangeFrequency()
        {
            return ChangeFrequency.HasValue;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        [Pure]
        public bool ShouldSerializeLastModifiedString()
        {
            return LastModified.HasValue;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [PublicAPI]
        [Pure]
        public bool ShouldSerializePriority()
        {
            return Priority.HasValue;
        }

        [PublicAPI]
        [Pure]
        public bool Equals(object p, IEqualityComparer comparer)
        {
            if (ReferenceEquals(p, null))
            {
                return false;
            }
            if (ReferenceEquals(this, p))
            {
                return true;
            }
            var other = p as Location;
            if (other == null)
            {
                return false;
            }
            // for comparing Urls, if one of them is absolute and the other relative, ignore the authority bit, just take the absolute path, so http://example.com:123/resourcesets/myset/resources/myres
            // comes out as equal to /resourcesets/myset/resources/myres
            if (Url.IsAbsoluteUri && !other.Url.IsAbsoluteUri)
            {
                if (!Url.AbsolutePath.Equals(other.Url.ToString()))
                {
                    return false;
                }
            }
            else if (!Url.IsAbsoluteUri && other.Url.IsAbsoluteUri)
            {
                if (!other.Url.AbsolutePath.Equals(Url.ToString()))
                {
                    return false;
                }
            }
            if (Url != other.Url)
            {
                return false;
            }
            if (Priority != other.Priority)
            {
                return false;
            }
            if (LastModified != other.LastModified)
            {
                return false;
            }
            return Metadata == other.Metadata;
        }

        [PublicAPI]
        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Location)obj);
        }

        [PublicAPI]
        [Pure]
        public static bool operator ==(Location left, Location right)
        {
            return Equals(left, right);
        }

        [PublicAPI]
        [Pure]
        public static bool operator !=(Location left, Location right)
        {
            return !Equals(left, right);
        }

        [Pure]
        [PublicAPI]
        public override string ToString()
        {
            return new { Url, ChangeFrequency, LastModified, Priority, Metadata }.ToString();
        }

        [PublicAPI]
        public int GetHashCode(IEqualityComparer comparer)
        {
            unchecked
            {
                var hashCode = ChangeFrequency.GetHashCode();
                hashCode = (hashCode * 397) ^ LastModified.GetHashCode();
                hashCode = (hashCode * 397) ^ Priority.GetHashCode();
                hashCode = (hashCode * 397) ^ (Url?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Metadata?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}