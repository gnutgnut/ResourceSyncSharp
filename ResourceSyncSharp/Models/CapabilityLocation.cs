using System;
using System.ComponentModel;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [PublicAPI]
    public sealed class CapabilityLocation : Location
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private CapabilityLocation()
        {
        }

        [PublicAPI]
        [Pure]
        public static CapabilityLocation Create(OperationCapabilities capability, Uri locationUrl)
        {
            return new CapabilityLocation { Url = locationUrl, Metadata = new ResourceSyncMetadata { Capability = capability.ToString() } };
        }
    }
}