using System.Threading.Tasks;

using JetBrains.Annotations;

namespace ResourceSyncSharp
{
    /// <summary>
    /// Equivalent of Task.CompletedTask, for use until we target .net 4.6
    /// https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.completedtask(v=vs.110).aspx
    /// </summary>
    [PublicAPI]
    public static class TaskEx
    {
        [PublicAPI]
        public static Task CompletedTask => Task.FromResult<object>(null);
    }
}