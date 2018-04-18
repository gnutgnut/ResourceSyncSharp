using System;

using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Extensions
{
    [PublicAPI]
    public static class RebasingExtension
    {
        [PublicAPI]
        public static ResourceSet MakeRelativeTo(this ResourceSet set, Uri source)
        {
            var sansHost = set.ToString().Replace(source.ToString(), "");
            var sansResourceSetsPath = sansHost.Replace(ResourceSyncDocument.UriPartNames.ResourceSets + "/", "");
            return sansResourceSetsPath;
        }

        [PublicAPI]
        public static Location MakeRelativeTo(this Location loc, Uri source)
        {
            return new Location
            {
                ChangeFrequency = loc.ChangeFrequency,
                LastModified = loc.LastModified,
                Metadata = loc.Metadata,
                Priority = loc.Priority,
                Url = new Uri(loc.Url.AbsolutePath, UriKind.Relative)
            };
        }
    }
}