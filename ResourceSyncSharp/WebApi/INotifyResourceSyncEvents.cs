namespace ResourceSyncSharp.WebApi
{
    public interface INotifyResourceSyncEvents : INotifyFrameworkEvent, INotifyChangeEvent, ISubscribeToResourceSet
    {
    }
}