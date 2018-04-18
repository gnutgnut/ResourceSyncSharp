using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [Serializable]
    [PublicAPI]
    public sealed class Link : IStructuralEquatable
    {
        [PublicAPI]
        public Link(Relations relation, HRef href)
        {
            HRef = href;
            Relation = relation;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Link()
        {
        }

        [PublicAPI]
        [XmlAttribute("href")]
        public string HRef
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [PublicAPI]
        [XmlIgnore]
        public Relations Relation
        {
            [PublicAPI]
            [Pure]
            get;
            set;
        }

        [PublicAPI]
        [XmlAttribute("rel")]
        public string RelationString
        {
            [PublicAPI]
            [Pure]
            get { return Relation.ToString(); }
            set { Relation = (Relations)Enum.Parse(typeof(Relations), value); }
        }

        [PublicAPI]
        [Pure]
        public static Link CreateUpLink(HRef href)
        {
            return new Link(Relations.up, href);
        }

        [PublicAPI]
        [Pure]
        public static Link CreateUpLink(Uri baseUri, HRef href)
        {
            return CreateUpLink(new Uri(baseUri, href).ToString());
        }

        [PublicAPI]
        public enum Relations
        {
            // ReSharper disable InconsistentNaming
            [PublicAPI]
            up,

            [PublicAPI]
            duplicate,

            [PublicAPI]
            describedby,

            [PublicAPI]
            index
            // ReSharper enable InconsistentNaming
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
            var other = p as Link;
            if (other == null)
            {
                return false;
            }
            if (HRef != other.HRef)
            {
                return false;
            }
            return Relation == other.Relation;
        }

        [PublicAPI]
        [Pure]
        public int GetHashCode(IEqualityComparer comparer)
        {
            return HRef.GetHashCode();
        }
    }
}