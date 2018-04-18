using System;
using System.Linq;

using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Extensions
{
    public static class LocationExtension
    {
        [PublicAPI]
        public static Tuple<ResourceSet, ResourceName> ResourceListLocationToResourceName(this Location loc)
        {
            var pieces = loc.Url.ToString().Split('/').Reverse().ToArray();
            var name = (ResourceName)pieces.First();
            var set = (ResourceSet)pieces.Skip(2).First();
            var setAndName = Tuple.Create(set, name);
            return setAndName;
        }
    }
}