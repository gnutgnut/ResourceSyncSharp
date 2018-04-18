using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

using ResourceSyncSharp.Codecs;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace ResourceSyncSharp.Models
{
    [Serializable]
    [PublicAPI]
    public sealed class ResourceSyncMetadata
    {
        /// <summary>shim for xml serialize of datetime</summary>
        [PublicAPI]
        [XmlAttribute("at")]
        // ReSharper disable once InconsistentNaming
        public string at
        {
            [PublicAPI]
            [Pure]
            get { return At.ToString(ResourceSyncDocument.W3CDateTimeFormat); }
            set { At = DateTime.ParseExact(value, ResourceSyncDocument.W3CDateTimeFormat, CultureInfo.InvariantCulture); }
        }

        [JsonIgnore]
        [PublicAPI]
        [XmlIgnore]
        public DateTime At
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }
        [JsonProperty("capability")]
        [PublicAPI]
        [XmlAttribute("capability")]
        public string Capability
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [JsonProperty("change")]
        [PublicAPI]
        [XmlAttribute("change")]
        public string Change
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        /// <summary>shim for xml serialize of datetime</summary>
        [PublicAPI]
        [XmlAttribute("from")]
        // ReSharper disable once InconsistentNaming
        public string from
        {
            [PublicAPI]
            [Pure]
            get { return From.ToString(ResourceSyncDocument.W3CDateTimeFormat); }
            set { From = DateTime.ParseExact(value, ResourceSyncDocument.W3CDateTimeFormat, CultureInfo.InvariantCulture); }
        }

        [JsonIgnore]
        [PublicAPI]
        [XmlIgnore]
        public DateTime From
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [JsonProperty("hash")]
        [PublicAPI]
        [XmlAttribute("hash")]
        public string Hash
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [JsonProperty("length")]
        [PublicAPI]
        [XmlAttribute("length")]
        public string Length
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [JsonProperty("properties")]
        [PublicAPI]
        [XmlAttribute("properties")]
        // ReSharper disable once InconsistentNaming
        public string properties
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        // slightly dirty hack to serialise arbitrary property dictionary,
        // plus map the special ones to their own fields, so if you set a property called 'Length' 
        // then it will come out in the .Length field, not in the property block. And same for 'Hash'.
        [XmlIgnore]
        [JsonIgnore]
        public Dictionary<string, string> Properties
        {
            set
            {
                var otherProperties = MapSpecialProperties(value);
                properties = JsonCodec.ToJson(otherProperties);
            }
            get { return properties == null ? null : JsonCodec.FromJson<Dictionary<string, string>>(properties); }
        }

        /// <summary>shim for xml serialize of datetime</summary>
        [PublicAPI]
        [XmlAttribute("until")]
        // ReSharper disable once InconsistentNaming
        public string until
        {
            [PublicAPI]
            [Pure]
            get { return Until.ToString(ResourceSyncDocument.W3CDateTimeFormat); }
            set { Until = DateTime.ParseExact(value, ResourceSyncDocument.W3CDateTimeFormat, CultureInfo.InvariantCulture); }
        }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime Until
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        #region odd casings here are correct - serializer looks for method of form ShouldSerialize{propertyName}
        [PublicAPI]
        [Pure]
        public bool ShouldSerializeat()
        {
            return At != DateTime.MinValue;
        }

        [PublicAPI]
        [Pure]
        public bool ShouldSerializefrom()
        {
            return From != DateTime.MinValue;
        }

        [PublicAPI]
        [Pure]
        public bool ShouldSerializehash()
        {
            return !string.IsNullOrWhiteSpace(Hash);
        }

        [PublicAPI]
        [Pure]
        public bool ShouldSerializelength()
        {
            return !string.IsNullOrWhiteSpace(Length);
        }

        [PublicAPI]
        [Pure]
        public bool ShouldSerializeproperties()
        {
            return Properties != null && Properties.Any();
        }

        [PublicAPI]
        [Pure]
        public bool ShouldSerializeuntil()
        {
            return Until != DateTime.MinValue;
        }
        #endregion

        private IDictionary<string, string> MapSpecialProperties(IDictionary<string, string> props)
        {
            if (props == null)
                return null;
            if (props.ContainsKey(SpecialKeys.Length))
            {
                Length = props[SpecialKeys.Length];
                props.Remove(SpecialKeys.Length);
            }
            // ReSharper disable once InvertIf
            if (props.ContainsKey(SpecialKeys.Hash))
            {
                Hash = props[SpecialKeys.Hash];
                props.Remove(SpecialKeys.Hash);
            }
            return props;
        }

        private static class SpecialKeys
        {
            internal const string Hash = "Hash";
            internal const string Length = "Length";
        }
    }
}