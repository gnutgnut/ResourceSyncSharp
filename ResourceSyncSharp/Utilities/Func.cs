using System;
using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Utilities
{
    /// <summary>
    /// This just enables usage like:
    /// 
    /// var myfunc = Func.Infer((o) => o.DoSomething());
    /// 
    /// i.e. infer the type of an anonymous delegate
    /// 
    /// that is all
    /// no need to panic
    /// you may resume your day
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Func
    {
        /// <typeparam name="TA">The type of the argument</typeparam>
        /// <typeparam name="TR">The type of the result</typeparam>
        /// <param name="f">The delegate</param>
        /// <returns>The very same delegate, but now with added inference goodness</returns>
        [PublicAPI]
        public static Func<TA, TR> Infer<TA, TR>(Func<TA, TR> f)
        {
            return f;
        }

        /// <typeparam name="TA1">The type of the first argument</typeparam>
        /// <typeparam name="TA2">The type of the second argument</typeparam>
        /// <typeparam name="TR">The type of the result</typeparam>
        /// <param name="f">The delegate</param>
        /// <returns>The very same delegate, but now with added inference goodness</returns>
        [PublicAPI]
        public static Func<TA1, TA2, TR> Infer<TA1, TA2, TR>(Func<TA1, TA2, TR> f)
        {
            return f;
        }

        /// <typeparam name="TA1">The type of the first argument</typeparam>
        /// <typeparam name="TA2">The type of the second argument</typeparam>
        /// <typeparam name="TA3">The type of the third argument</typeparam>
        /// <typeparam name="TR">The type of the result</typeparam>
        /// <param name="f">The delegate</param>
        /// <returns>The very same delegate, but now with added inference goodness</returns>
        [PublicAPI]
        public static Func<TA1, TA2, TA3, TR> Infer<TA1, TA2, TA3, TR>(Func<TA1, TA2, TA3, TR> f)
        {
            return f;
        }
    }
}