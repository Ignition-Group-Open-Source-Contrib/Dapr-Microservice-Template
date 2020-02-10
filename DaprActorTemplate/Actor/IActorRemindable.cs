using System;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;

namespace DaprActorTemplate.Actor
{
    public interface IRemindableWrapper
    {
        Task<IActorReminder> RegisterReminderAsync(
            string reminderName,
            byte[] state,
            TimeSpan dueTime,
            TimeSpan period);
    }
}