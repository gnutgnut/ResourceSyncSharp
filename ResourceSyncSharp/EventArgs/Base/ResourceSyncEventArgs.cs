using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using ResourceSyncSharp.Models;

using JetBrains.Annotations;

namespace ResourceSyncSharp.EventArgs.Base
{
    [Serializable]
    public abstract class ResourceSyncEventArgs : System.EventArgs
    {
        protected ResourceSyncEventArgs(ResourceSyncDocument rsDoc)
        {
            FrameworkEventDocument = rsDoc;
        }

        /// <summary>
        /// (this is public only for the serialization - don't set!)
        /// </summary>
        [PublicAPI]
        public ResourceSyncDocument FrameworkEventDocument
        {
            [Pure]
            get;
            [EditorBrowsable(EditorBrowsableState.Never)]
            set;
        }

        [PublicAPI]
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            if (FrameworkEventDocument == null)
            {
                return "== null rsevent ==";
            }
            return FrameworkEventDocument.XmlCodec.ToXml();
        }
    }
}