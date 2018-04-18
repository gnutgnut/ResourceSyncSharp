using System;

using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

using static System.String;

using static ResourceSyncSharp.Models.ResourceSyncDocument.UriPartNames;

using static System.UriKind;

namespace ResourceSyncSharp.Extensions
{
    public static class StringExtension
    {
        [PublicAPI]
        public static string AsResourceListRelativeSubLocation(this string resourceSet)
        {
            return Join("/", resourceSet, ResourceList);
        }

        [PublicAPI]
        public static string AsResourceLocation(this string resourceSet, string resourceName)
        {
            return Join("/", resourceSet, Resources, resourceName);
        }

        [PublicAPI]
        [Pure]
        public static string AsResourceSetRelativeSuperLocation(this string resourceSet)
        {
            return Join("/", ResourceSets, resourceSet);
        }

        [PublicAPI]
        [Pure]
        public static Uri ToGetUri(this ResourceSet set, ResourceName name)
        {
            return ToGetUri(set.ToString(), name.ToString());
        }

        [PublicAPI]
        [Pure]
        public static Uri ToGetUri(this string setName, string resourceName)
        {
            return new Uri(setName.ToGetUrlString(resourceName), Relative);
        }

        [PublicAPI]
        [Pure]
        public static Uri ToLocationUri(this ResourceSet set)
        {
            return ToLocationUri(set.ToString());
        }

        [PublicAPI]
        [Pure]
        public static Uri ToLocationUri(this string setName)
        {
            return new Uri(setName.ToLocationUrlString(), Relative);
        }

        [PublicAPI]
        [Pure]
        public static Uri ToPutUri(this ResourceSet set)
        {
            return ToPutUri(set.ToString());
        }

        [PublicAPI]
        [Pure]
        public static Uri ToPutUri(this ResourceSet set, ResourceName name)
        {
            return ToPutUri(set.ToString(), name.ToString());
        }

        [PublicAPI]
        [Pure]
        public static Uri ToPutUri(this string setName)
        {
            return new Uri(setName.ToPutUrlString(), Relative);
        }

        [PublicAPI]
        [Pure]
        public static Uri ToPutUri(this string setName, string resourceName)
        {
            return new Uri(setName.ToPutUrlString(resourceName), Relative);
        }

        [PublicAPI]
        [Pure]
        public static ResourceSyncDocument.RequestDescriptor ToSyncRequest(this ResourceSet set)
        {
            return set.ToString().ToSyncRequest();
        }

        [PublicAPI]
        [Pure]
        public static ResourceSyncDocument.RequestDescriptor ToSyncRequest(this string setName)
        {
            return setName.ToLocationUri().ToSyncRequest(OperationCapabilities.resourcelist);
        }
    }
}