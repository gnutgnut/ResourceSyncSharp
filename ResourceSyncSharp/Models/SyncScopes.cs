using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [PublicAPI]
    public enum SyncScopes
    {
        /// <summary>
        /// Sync based only on the resourcesync document at the location passed.
        /// </summary>
        [PublicAPI]
        LocationOnly,

        /// <summary>
        /// Sync the whole source, even if the location passed is only a part of it, so if the location
        /// is a capability list, the sync will first pull the parent source description then sync from that,
        /// or if the location is a resource list, the sync will first pull the parent capability list, then
        /// pull the parent source description, then sync from that.
        /// </summary>
        [PublicAPI]
        WholeSource
    }
}