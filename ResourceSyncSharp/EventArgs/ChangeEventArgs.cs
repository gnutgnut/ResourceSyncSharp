using System;
using System.ComponentModel;

using ResourceSyncSharp.EventArgs.Base;
using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.EventArgs
{
    [Serializable]
    public sealed class ChangeEventArgs : ResourceSyncEventArgs
    {
        [PublicAPI]
        public ChangeEventArgs(ResourceSet set, ResourceName name, ResourceSyncDocument rsDoc) : base(rsDoc)
        {
            Set = set;
            Name = name;
        }

        [PublicAPI]
        public ResourceName Name
        {
            get;
            [EditorBrowsable(EditorBrowsableState.Never)]
            set;
        }

        [PublicAPI]
        public ResourceSet Set
        {
            get;
            [EditorBrowsable(EditorBrowsableState.Never)]
            set;
        }
    }
}