using ResourceSyncSharp.EventArgs;

using JetBrains.Annotations;

namespace ResourceSyncSharp.WebApi
{
    public interface INotifyChangeEvent
    {
        [PublicAPI]
        void NotifyChangeEvent(ChangeEventArgs changeEventArgs);
    }
}