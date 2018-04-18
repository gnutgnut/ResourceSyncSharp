using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    [PublicAPI]
    public enum OperationCapabilities
    {
        // ReSharper disable InconsistentNaming
        [PublicAPI]
        description,

        [PublicAPI]
        capabilitylist,

        [PublicAPI]
        resourcelist,

        [PublicAPI]
        resourcedump,

        [PublicAPI]
        changelist,

        [PublicAPI]
        changedump
    }
}