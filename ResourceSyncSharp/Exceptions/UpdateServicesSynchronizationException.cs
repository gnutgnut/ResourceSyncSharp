using System;
using System.Runtime.Serialization;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Exceptions
{
    /// <summary>
    /// Used to report a fault occuring during an update services synchronization operation.
    /// </summary>
    [Serializable]
    [PublicAPI]
    public class SynchronizationException : Exception
    {
        [PublicAPI]
        public SynchronizationException()
        {
        }

        [PublicAPI]
        public SynchronizationException(string message) : base(message)
        {
        }

        [PublicAPI]
        public SynchronizationException(string message, Exception inner) : base(message, inner)
        {
        }

        [PublicAPI]
        protected SynchronizationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}