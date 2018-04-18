using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ResourceSyncSharp.EventArgs;
using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp
{
    [PublicAPI]
    public interface IProvideResources : IDisposable
    {
        /// <summary>
        /// Occurs if the capability list, change list or resource list for this resource set has changed,
        /// OR if the overall source description has changed (this is broadcast to all clients, no matter
        /// what resource set they have subscribed to.
        /// </summary>
        event EventHandler<FrameworkEventArgs> FrameworkChanged;

        /// <summary>
        /// Occurs if a resource in this resource set has changed (created, updated, deleted)
        /// </summary>
        event EventHandler<ChangeEventArgs> ResourceChanged;

        /// <summary>
        /// All the resources in this provider
        /// </summary>
        Task<IEnumerable<Tuple<ResourceSet, Location, ResourceValue>>> GetAllResourcesAsync();

        /// <summary>
        /// True if this provider will fire events. If it doesn't, you will need to poll it.
        /// NB: Even when this is true, the provider still fires events when changes are made
        /// via the provider itself, it just doesn't spontaneously fire them when external actors make changes.
        /// </summary>
        bool IsAsyncEventSource { get; }

        Task DeleteResourceAsync(ResourceSet set, ResourceName name, Uri baseUri);

        Task DeleteResourceSetAsync(ResourceSet set, Uri baseUri);

        Task<ResourceSyncDocument> GetCapabilityListAsync(Uri baseUri, ResourceSet resourceSet);

        Task<ResourceSyncDocument> GetChangeListAsync(Uri baseUri, ResourceSet resourceset);

        Task<ResourceSyncDocument> GetResourceListAsync(Uri baseUri, ResourceSet resourceset);

        Task<ResourceValue> GetResourceValueAsync(ResourceSet resourceSet, ResourceName name);

        Task<ResourceSyncDocument> GetSourceDescriptionAsync(Uri baseUri);

        Task PutResourceAsync(ResourceSet resourceset, ResourceName resourceName, ResourceValue resourceValue, [CanBeNull] IDictionary<string,string> properties, Uri baseUri, bool fireEvents);

        Task PutResourceSetAsync(ResourceSet resourceset, Uri baseUri);

        Task SaveResourceAsync(ResourceSet set, Location loc, ResourceValue val, Uri baseUri);
    }
}