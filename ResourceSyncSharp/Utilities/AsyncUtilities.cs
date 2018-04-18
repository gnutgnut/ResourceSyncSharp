using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace ResourceSyncSharp.Utilities
{
    public static class AsyncUtilities
    {
        /// <summary>
        /// Runs a Func. If the func throws an aggregateException, rethrows the first inner exception
        /// with stack trace intact.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="f">f</param>
        /// <returns>the T</returns>
        public static T Disaggregate<T>(Func<T> f)
        {
            try
            {
                return f();
            }
            catch (AggregateException ex)
            {
                Trace.WriteLine(ex.ToString());
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerExceptions.First()).Throw();
                return default(T); // never happens but compiler needs it
            }
        }
    }
}