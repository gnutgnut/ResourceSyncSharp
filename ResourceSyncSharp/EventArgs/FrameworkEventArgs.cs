using System;
using System.ComponentModel;

using ResourceSyncSharp.EventArgs.Base;
using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.EventArgs
{
    [Serializable]
    public sealed class FrameworkEventArgs : ResourceSyncEventArgs
    {
        [PublicAPI]
        public FrameworkEventArgs(ResourceSyncDocument rsDoc, ResourceSet set = null) : base(rsDoc)
        {
            Set = set;
        }

        [PublicAPI]
        public bool IsGlobal => Set == null;

        [PublicAPI]
        public ResourceSet Set
        {
            get;
            [EditorBrowsable(EditorBrowsableState.Never)]
            set;
        }

        [PublicAPI]
        public T Map<T>(Func<T> forGlobal, Func<T> forLocal)
        {
            return IsGlobal ? forGlobal() : forLocal();
        }
    }
}