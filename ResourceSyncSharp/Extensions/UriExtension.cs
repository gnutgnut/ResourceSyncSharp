using System;

using ResourceSyncSharp.Models;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Extensions
{
    public static class UriExtension
    {
        /// <summary>
        /// With this you can turn a Uri into a synchronisation request.
        /// NB: The default capability expects the Uri to point to the well-known source description (/.well-known/resourcesync),
        /// be sure to override the cap param to correspond with the document type at that url on the source, otherwise the validation
        /// will reject the request
        /// </summary>
        /// <param name="uri">Uri of a resourcesync document on a source</param>
        /// <param name="cap">The type of resourcesync document expected at that url</param>
        /// <param name="syncScope"></param>
        /// <returns></returns>
        [PublicAPI]
        public static ResourceSyncDocument.RequestDescriptor ToSyncRequest(this Uri uri,
                                                                           OperationCapabilities cap = OperationCapabilities.description,
                                                                           SyncScopes syncScope = SyncScopes.LocationOnly)
        {
            return new ResourceSyncDocument.RequestDescriptor(uri, cap, syncScope);
        }
    }
}