using JetBrains.Annotations;

namespace ResourceSyncSharp.Extensions
{
    public static class ResourceSetExtension
    {
        [Pure]
        [PublicAPI]
        public static string ToGetUrlString(this string setName, string resourceName)
        {
            return setName.AsResourceSetRelativeSuperLocation().AsResourceLocation(resourceName);
        }

        [Pure]
        [PublicAPI]
        public static string ToLocationUrlString(this string setName)
        {
            return setName.AsResourceSetRelativeSuperLocation().AsResourceListRelativeSubLocation();
        }

        [Pure]
        [PublicAPI]
        public static string ToPutUrlString(this string setName)
        {
            return setName.AsResourceSetRelativeSuperLocation();
        }

        [Pure]
        [PublicAPI]
        public static string ToPutUrlString(this string setName, string resourceName)
        {
            return setName.AsResourceSetRelativeSuperLocation().AsResourceLocation(resourceName);
        }
    }
}