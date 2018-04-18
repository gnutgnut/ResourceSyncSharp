using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.WebApi
{
    public interface ISubscribeToResourceSet
    {
        [PublicAPI]
        void Subscribe(ResourceSet resourceSet);

        [PublicAPI]
        void UnSubscribe(ResourceSet resourceSet);
    }
}