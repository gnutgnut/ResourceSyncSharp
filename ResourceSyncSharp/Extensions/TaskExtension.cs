using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Extensions
{
    public static class TaskExtension
    {
        [PublicAPI]
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout, string opname, bool configureAwait = false)
        {
            var opid = Guid.NewGuid();
            Trace.WriteLine(new { op=nameof(TimeoutAfter), timeout, opname, opid });
            var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token)).ConfigureAwait(configureAwait);
            if (completedTask != task)
            {
                string err = "The " + opname+ " operation has timed out.";
                Trace.WriteLine(new { op = nameof(TimeoutAfter), timeout, err, opid });
                throw new TimeoutException(err);
            }
            Trace.WriteLine(new { op = nameof(TimeoutAfter), timeout, opname, opid, res="OK" });
            timeoutCancellationTokenSource.Cancel();
            return await task.ConfigureAwait(configureAwait);
        }
    }
}