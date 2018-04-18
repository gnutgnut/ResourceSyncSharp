using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [PublicAPI]
    public sealed class ChangeLocation : Location
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private ChangeLocation()
        {
        }

        [PublicAPI]
        [Pure]
        public static ChangeLocation Create(Uri loc, DateTime lastmod, ChangeTypes change, ChangeFrequencies? changeFrequency = null, IDictionary<string,string> properties = null)
        {
            return new ChangeLocation
            {
                Url = loc,
                LastModified = lastmod,
                Metadata = new ResourceSyncMetadata { Change = change.ToString(), Properties = properties?.ToDictionary(kvp=>kvp.Key,kvp=>kvp.Value) },
                ChangeFrequency = changeFrequency,
                Priority = 1
            };
        }
    }
}