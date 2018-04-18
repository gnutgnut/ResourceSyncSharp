using ResourceSyncSharp.EventArgs;

using JetBrains.Annotations;

namespace ResourceSyncSharp.WebApi
{
    public interface INotifyFrameworkEvent
    {
        [PublicAPI]
        void NotifyFrameworkEvent(FrameworkEventArgs frameworkEventArgs);
    }
}