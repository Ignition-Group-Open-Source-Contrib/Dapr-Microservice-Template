using System;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;

namespace AgentCallbackActor.Actor
{
    public class RemindableWrapper : IRemindableWrapper
    {
        private readonly Func<string, byte[], TimeSpan, TimeSpan, Task<IActorReminder>> reminderAction;

        public RemindableWrapper(Func<string, byte[], TimeSpan, TimeSpan, Task<IActorReminder>> reminderAction)
        {
            this.reminderAction = reminderAction;
        }
        public async Task<IActorReminder> RegisterReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {
            return await reminderAction.Invoke(reminderName, state, dueTime, period);
        }
    }
}