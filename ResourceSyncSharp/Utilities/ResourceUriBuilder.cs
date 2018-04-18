using System;

using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

using static ResourceSyncSharp.Models.ResourceSyncDocument.UriPartNames;

using static System.UriKind;

namespace ResourceSyncSharp.Utilities
{
    [PublicAPI]
    public static class ResourceUriBuilder
    {
        [PublicAPI]
        public static Uri MakeResourceListUri(ResourceSet set) => new Uri($"/{ResourceSets}/{set}/resourcelist", Relative);

        [PublicAPI]
        public static Uri MakeResourceSetUri(ResourceSet set) => new Uri($"/{ResourceSets}/{set}", Relative);

        [PublicAPI]
        public static Uri MakeResourceUri(ResourceSet set, ResourceName name) => new Uri($"/{ResourceSets}/{set}/{Resources}/{name}", Relative);
    }
}