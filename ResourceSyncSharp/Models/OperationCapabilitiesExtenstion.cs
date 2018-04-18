using System;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    public static class OperationCapabilitiesExtenstion
    {
        [Pure]
        [PublicAPI]
        public static T MapCapability<T>(this OperationCapabilities cap, Func<T> onResourceList, Func<T> onCapList)
        {
            switch (cap)
            {
                case OperationCapabilities.resourcelist:
                    return onResourceList();
                case OperationCapabilities.capabilitylist:
                    return onCapList();
            }
            throw new InvalidOperationException("no such capability: " + cap);
        }
    }
}