using System;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Models
{
    public static class ChangeTypeExtension
    {
        [PublicAPI]
        public static void Map(this ChangeTypes changeType, Action onCreated, Action onUpdated, Action onDeleted)
        {
            switch (changeType)
            {
                case ChangeTypes.created:
                    onCreated();
                    return;
                case ChangeTypes.deleted:
                    onDeleted();
                    return;
                case ChangeTypes.updated:
                    onUpdated();
                    return;
                default:
                    throw new InvalidOperationException("Can't map unknown change type: " + changeType);
            }
        }

        [PublicAPI]
        public static T Map<T>(this ChangeTypes changeType, Func<T> onCreated, Func<T> onUpdated, Func<T> onDeleted)
        {
            switch (changeType)
            {
                case ChangeTypes.created:
                    return onCreated();
                case ChangeTypes.deleted:
                    return onDeleted();
                case ChangeTypes.updated:
                    return onUpdated();
                default:
                    throw new InvalidOperationException("Can't map unknown change type: " + changeType);
            }
        }
    }
}