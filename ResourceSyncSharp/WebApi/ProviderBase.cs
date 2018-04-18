using System;
using System.Diagnostics;
using System.Linq;

using ResourceSyncSharp.EventArgs;
using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

using Microsoft.AspNet.SignalR;

namespace ResourceSyncSharp.WebApi
{
    public abstract class ProviderBase
    {
        private const string Class = "provbase";

        [PublicAPI]
        protected ProviderBase(IHubContext hubContext)
        {
            const string op = nameof(ProviderBase);
            if (hubContext == null)
            {
                Trace.WriteLine(new { cls = Class, op, res = "no hubcontext" });
                return;
            }
            FrameworkChanged += (sender, args) =>
            {
                Trace.WriteLine(new { cls = Class, id = GetHashCode(), op = "FRAMEWORKCHANGED" });
                // a framework event (about a change to the source, or to one of its resource sets)
                // this should be broadcast to all interested parties:
                args.Map(
                    // - changes to the source itself (new, updated or removed capability list) go to everyone
                    () => hubContext.Clients.All.NotifyFrameworkEvent(args),
                    () =>
                    {
                        // - changes to a single resource set go to only those that have registered an interest in that set
                        hubContext.Clients.Group(args.Set.ToString()).NotifyFrameworkEvent(args);
                        // - and to those who have registered an interest in all sets ("*")
                        return hubContext.Clients.Group("*").NotifyFrameworkEvent(args);
                    });
            };
            ResourceChanged += (sender, args) =>
            {
                var group = args.Set.ToString();
                Trace.WriteLine(new { cls = Class, id = GetHashCode(), op = "RESOURCECHANGED", group });
#if GROUPS_WORK // signalR groups not working properly under unit test - they are supposed to work when hosted in IIS
                // - resource changes go only to clients interested in that resource set
                hubContext.Clients.Group(group).NotifyChangeEvent(args);
                // and to those that have registered an interest in all sets ("*")
                hubContext.Clients.Group("*").NotifyChangeEvent(args);
#else
                // just send them to all clients - still works, just means some clients will get woken up only to ignore what's plainly not for them
                hubContext.Clients.All.NotifyChangeEvent(args);
#endif
            };
        }

        [PublicAPI]
        public event EventHandler<FrameworkEventArgs> FrameworkChanged;
        [PublicAPI]
        public event EventHandler<ChangeEventArgs> ResourceChanged;

        [System.Diagnostics.Contracts.Pure]
        protected static Location.ChangeFrequencies ChangeFrequencyFromTimespan(TimeSpan ts)
        {
            if (ts == TimeSpan.MaxValue)
            {
                return Location.ChangeFrequencies.never;
            }
            if (ts.TotalDays > 365)
            {
                return Location.ChangeFrequencies.yearly;
            }
            if (ts.TotalDays > 28)
            {
                return Location.ChangeFrequencies.monthly;
            }
            if (ts.TotalDays > 6)
            {
                return Location.ChangeFrequencies.weekly;
            }
            if (ts.TotalDays > 1)
            {
                return Location.ChangeFrequencies.daily;
            }
            return Location.ChangeFrequencies.hourly;
        }

        [PublicAPI]
        protected void FireFrameworkChangedEvent(Uri serverBaseUri, ResourceSet set, params ChangeLocation[] changeLocations)
        {
            const string op = nameof(FireFrameworkChangedEvent);
            var handlers = FrameworkChanged;
            if (handlers == null)
            {
                Trace.WriteLine(new { cls = Class, op, set, locations = changeLocations.Count(), handlerCount = -1 });
                return;
            }
            Trace.WriteLine(new { cls = Class, op, locations = changeLocations.Count(), handlerCount = handlers.GetInvocationList().Count() });
            var frameworkNote = ResourceSyncDocument.Factory.CreateFrameworkNotification(serverBaseUri, serverBaseUri, set, changeLocations);
            handlers(this, new FrameworkEventArgs(frameworkNote, set));
        }

        [PublicAPI]
        protected void FireGlobalFrameworkChangedEvent(Uri serverBaseUri, params ChangeLocation[] locations)
        {
            FireFrameworkChangedEvent(serverBaseUri, null, locations);
        }

        [PublicAPI]
        protected void FireResourceChangedEvent(Uri serverBaseUri, ResourceSet set, ResourceName name, params ChangeLocation[] changeLocations)
        {
            const string op = nameof(FireResourceChangedEvent);
            var handlers = ResourceChanged;
            if (handlers == null)
            {
                Trace.WriteLine(new { cls = Class, op, set, locations = changeLocations.Count(), handlerCount = -1 });
                return;
            }
            Trace.WriteLine(new { cls = Class, op, locations = changeLocations.Count(), handlerCount = handlers.GetInvocationList().Count() });
            var changeNote = ResourceSyncDocument.Factory.CreateChangeNotification(serverBaseUri, serverBaseUri, set, changeLocations);
            handlers(this, new ChangeEventArgs(set, name, changeNote));
        }
    }
}