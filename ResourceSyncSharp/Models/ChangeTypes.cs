using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [PublicAPI]
    public enum ChangeTypes
    {
        // ReSharper disable InconsistentNaming
        [PublicAPI]
        created,

        [PublicAPI]
        updated,

        [PublicAPI]
        deleted
    }
}