using System;
using System.Collections.Generic;
using System.Diagnostics;

using ResourceSyncSharp.Extensions;
using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp
{
    /// <summary>
    /// An immutable DTO for everything needed to describe a required sync operation - with a fluent builder for constructing it.
    /// </summary>
    public struct SyncContext
    {
        [PublicAPI]
        public readonly Func<ResourceSet, Location, IsOfInterest> IsInterestingChange;

        [PublicAPI]
        public readonly Func<ResourceSet, IList<Location>, IsOfInterest> IsInterestingSet;

        [PublicAPI]
        public readonly IProvideResources Provider;

        [PublicAPI]
        public readonly ResourceSyncDocument.RequestDescriptor ResourceSyncDocumentRequestDescriptor;

        [PublicAPI]
        public readonly Action<ResourceSet, Location, ResourceValue> SaveResource;

        [PublicAPI]
        public readonly Uri Source;

        [PublicAPI]
        public readonly SyncDirections SyncDirection;

        [PublicAPI]
        public readonly SyncScopes SyncScope;

        /// <summary>
        /// Private constructor (build instances using the builder)
        /// </summary>
        private SyncContext(Uri source,
                            ResourceSyncDocument.RequestDescriptor resourceSyncDocumentRequestDescriptor,
                            Func<ResourceSet, IList<Location>, IsOfInterest> isInterestingSet,
                            Func<ResourceSet, Location, IsOfInterest> isInterestingChange,
                            Action<ResourceSet, Location, ResourceValue> saveResource,
                            SyncScopes syncScope = SyncScopes.LocationOnly,
                            SyncDirections syncDirection = SyncDirections.SyncClientFromServer,
                            IProvideResources provider = null)
        {
            Source = source;
            ResourceSyncDocumentRequestDescriptor = resourceSyncDocumentRequestDescriptor;
            IsInterestingSet = isInterestingSet;
            IsInterestingChange = isInterestingChange;
            SaveResource = saveResource;
            SyncScope = syncScope;
            SyncDirection = syncDirection;
            Provider = provider;
        }

        [PublicAPI]
        public enum SyncDirections
        {
            SyncServerFromClient,
            SyncClientFromServer
        }

        /// <summary>
        /// A builder for a sync context, with sensible baked in defaults, and a debug assert on creation if something
        /// essential is missing. The only mandatory calls are to SaveResourcesTo, supplying a callback for saving of 
        /// resources, and Source, supplying the source server address - if this is all you specify, other options get
        /// the defaults, so this will result in a standard whole-source baseline sync.
        /// </summary>
        public sealed class Builder
        {
            [PublicAPI]
            public static readonly ResourceSyncDocument.RequestDescriptor SourceDescriptionDescriptor =
                new Uri(ResourceSyncDocument.UriPartNames.WellKnownResourceSync, UriKind.Relative).ToSyncRequest();

            /// <summary>
            /// This is the callback to check if a specific resource is of interest to the caller.
            /// The default implementation is for all resources to be of interest.
            /// NB: If the caller already returned false to 'isInterestingSet', they will not see
            /// any isInterestingChange calls for this set.
            /// </summary>
            private Func<ResourceSet, Location, IsOfInterest> _isInterestingResource = (set, loc) => true;

            /// <summary>
            /// This is the callback to check if a resource set is of interest to the caller.
            /// The default implementation is for all resource sets to be of interest.
            /// </summary>
            private Func<ResourceSet, IList<Location>, IsOfInterest> _isInterestingSet = (set, list) => true;

            private IProvideResources _provider;

            /// <summary>
            /// This is the resourcesync document request descriptor.
            /// The default descriptor is to do a sync of the whole source, using the standard well-known entry point
            /// </summary>
            private ResourceSyncDocument.RequestDescriptor? _resourceSyncDocumentRequestDescriptor = SourceDescriptionDescriptor;

            /// <summary>
            /// This is the callback to actually save a downloaded resource.
            /// The default implementation is to ignore the save.
            /// NB: If the caller already returned false to either 'isInterestingSet' or 'isInterestingChange', they
            /// will not see a saveResource call for this resource.
            /// </summary>
            private Action<ResourceSet, Location, ResourceValue> _saveResource = (set, location, value) => { };

            /// <summary>
            /// This is the net location of the source to sync to. It has no default and must be set during construction.
            /// </summary>
            private Uri _serverUri;

            private SyncDirections _syncDirection = SyncDirections.SyncClientFromServer;

            /// <summary>
            /// This is the sync scope, which is of relevance for any request descriptor except the default source-description.
            /// The default implementation is to sync only the specified location. It is possible to set it to 'whole source', which
            /// will cause the sync to hop up the tree until it finds the source-description, then sync from there - this means even though
            /// you requested to sync a specific resource or capability list, you will actually get the sync as if you had synced against
            /// the whole source.
            /// </summary>
            private SyncScopes _syncScope = SyncScopes.LocationOnly;

            /// <summary>
            /// Realise the sync context.
            /// </summary>
            /// <returns>A SyncContext embodying the builders baked in defaults and the users specific settings.</returns>
            [PublicAPI]
            [Pure]
            public SyncContext Build()
            {
                Debug.Assert(_serverUri != null);
                Debug.Assert(_resourceSyncDocumentRequestDescriptor != null);
                Debug.Assert(_isInterestingResource != null);
                Debug.Assert(_isInterestingSet != null);
                Debug.Assert(_saveResource != null || _syncDirection == SyncDirections.SyncServerFromClient);
                Debug.Assert(_provider != null || _syncDirection == SyncDirections.SyncClientFromServer);
                return new SyncContext(
                    _serverUri,
                    _resourceSyncDocumentRequestDescriptor.Value,
                    _isInterestingSet,
                    _isInterestingResource,
                    _saveResource,
                    _syncScope,
                    _syncDirection,
                    _provider);
            }

            /// <summary>
            /// OPTIONAL: Configure the descriptor for the resource sync document that you wish to use to execute your sync operation.
            /// DEFAULT: Sync the whole source.
            /// </summary>
            /// <param name="resourceSyncDocumentRequestDescriptor">This descriptor comprises a uri to find the resourcesync document and a capability to identify what kind of resourcesync document we expect to find there.
            /// It is used to define the scope of the sync operation, be that a single resource list, a whole source etc</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder Do(ResourceSyncDocument.RequestDescriptor resourceSyncDocumentRequestDescriptor)
            {
                _resourceSyncDocumentRequestDescriptor = resourceSyncDocumentRequestDescriptor;
                return this;
            }

            /// <summary>
            /// OPTIONAL: Configure resource filtering function.
            /// DEFAULT: The built in behaviour is to always download.
            /// </summary>
            /// <param name="isInterestingResource">This function is called each time the destination has detected a resource that it can download, to allow
            /// the vetoing or filtering of resources, for example if we already have them, or if we are just not interested
            /// in them for some other reason.</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder If(Func<ResourceSet, Location, IsOfInterest> isInterestingResource)
            {
                _isInterestingResource = isInterestingResource;
                return this;
            }

            /// <summary>
            /// OPTIONAL: Configure resource set filtering function.
            /// DEFAULT: The built in behaviour is to always proceed to sync the set.
            /// </summary>
            /// <param name="isInterestingSet">This function is called each time the destination has detected a resource that it can download, to allow
            /// the vetoing or filtering of resources, for example if we already have them, or if we are just not interested
            /// in them for some other reason.</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder IfResourceSet(Func<ResourceSet, IList<Location>, IsOfInterest> isInterestingSet)
            {
                _isInterestingSet = isInterestingSet;
                return this;
            }

            [PublicAPI]
            public Builder LoadResourcesFrom(IProvideResources provider)
            {
                _provider = provider;
                _syncDirection = SyncDirections.SyncServerFromClient;
                return this;
            }

            /// <summary>
            /// OPTIONAL: Configure the callback used to pass a downloaded resource value to your repository
            /// DEFAULT: no-op (in case you just want the resource list callback and are not saving or whatever)
            /// </summary>
            /// <param name="saveResource">Function to which downloaded resources will be passed</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder SaveResourcesTo(Action<ResourceSet, Location, ResourceValue> saveResource)
            {
                _saveResource = saveResource;
                _syncDirection = SyncDirections.SyncClientFromServer;
                return this;
            }

            /// <summary>
            /// OPTIONAL: Set the overall sync scope
            /// DEFAULT: The baked in beahviour is to sync only the document you request, not the whole source.
            /// </summary>
            /// <param name="syncScope">The scope of the sync operation - just the requested, or the whole shebang</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder SetSyncScope(SyncScopes syncScope)
            {
                _syncScope = syncScope;
                return this;
            }

            [Pure]
            [PublicAPI]
            public override string ToString()
            {
                return
                    new { _syncDirection, _serverUri, _syncScope, _resourceSyncDocumentRequestDescriptor, _isInterestingSet, _isInterestingResource, _saveResource }
                        .ToString();
            }

            /// <summary>
            /// MANDATORY: Set the Uri for this client to sync from/to
            /// </summary>
            /// <param name="serverUri">Uri of the Server webservice</param>
            /// <returns>Fluently</returns>
            [PublicAPI]
            public Builder WithServerAt(Uri serverUri)
            {
                _serverUri = serverUri;
                return this;
            }
        }
    }
}