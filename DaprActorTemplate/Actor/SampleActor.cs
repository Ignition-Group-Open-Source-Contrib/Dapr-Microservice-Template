// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprActorTemplate.Actor
{
    /// <summary>
    /// Actor Implementation.
    /// Following example shows how to use Actor Reminders as well.
    /// For Actors to use Reminders, it must derive from IRemindable.
    /// If you don't intend to use Reminder feature, you can skip implementing IRemindable and reminder specific methods which are shown in the code below.
    /// </summary>
    public class SampleActor : Dapr.Actors.Runtime.Actor, ISampleActor, IRemindable
    {
        private readonly string configSetting1;
        private readonly string configSetting2;
        private readonly IRemindableWrapper remindableWrapper;
        private readonly IActorStateManager actorStateManager;
        private const string StateName = "callbackstate";
        private const string ReminderName = "callbacknotificationreminder";

        
        public SampleActor(ActorHost host
                , IRemindableWrapper remindableWrapper = null
                , IActorStateManager actorStateManager = null
              )

            : base(host)
        {
            this.configSetting1 = "configSetting1";
            this.configSetting2 = "configSetting2";
            this.remindableWrapper = remindableWrapper ?? new RemindableWrapper(RegisterReminderAsync);
            this.actorStateManager = actorStateManager ?? this.StateManager;

        }

        private ApiResponse BuildResponse(string message, bool isSuccess)
        {
            return new ApiResponse() { IsError = isSuccess, Message = message };
        }
        /// <inheritdoc/>
        public async Task<ApiResponse> SaveStateData(StateData callbackStateInfo)
        {

            if (callbackStateInfo == default)
            {
                return BuildResponse("CallBackStateData cannot be null.", false);
            }

            if (string.IsNullOrEmpty(callbackStateInfo.FirstName))
            {
                return BuildResponse("CallBackTime cannot be empty.", false);
            }

            if (string.IsNullOrEmpty(callbackStateInfo.AnotherStringProperty))
            {
                return BuildResponse("PhoneNumber cannot be empty.", false);
            }

            if (string.IsNullOrEmpty(callbackStateInfo.StringProperty))
            {
                return BuildResponse("AgentId cannot be empty.", false);

            }

            if (configSetting1.Equals(configSetting2))
            {
                await this.actorStateManager.RemoveStateAsync(StateName);
            }
            await this.actorStateManager.SetStateAsync(StateName, callbackStateInfo);


            return BuildResponse("success", true);
        }

        /// <inheritdoc/>
        public async Task<StateData> GetReminderTime()
        {
            var callBackStateData = await actorStateManager.GetStateAsync<StateData>(StateName);
            return callBackStateData;
        }

        /// <inheritdoc/>
        public Task TestThrowException()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task TestNoArgumentNoReturnType()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task RegisterReminder()
        {
            await this.remindableWrapper.RegisterReminderAsync(ReminderName, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        /// <inheritdoc/>
        public Task UnregisterReminder()
        {
            return this.UnregisterReminderAsync(ReminderName);
        }

        /// <inheritdoc/>
        public async Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
        {

            var callBackStateData = await this.actorStateManager.GetStateAsync<StateData>(StateName);

            var stringContent = new StringContent(JsonSerializer.Serialize(callBackStateData), Encoding.UTF8, "application/json");

        }

        /// <inheritdoc/>
        public Task RegisterTimer()
        {
            return this.RegisterTimerAsync(ReminderName, nameof(this.TimerCallBack), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        /// <inheritdoc/>
        public Task UnregisterTimer()
        {
            return this.UnregisterTimerAsync(ReminderName);
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        protected override Task OnActivateAsync()
        {
            // Provides opportunity to perform some optional setup.
            return Task.CompletedTask;
        }

        /// <summary>
        /// This method is called whenever an actor is deactivated after a period of inactivity.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        protected override Task OnDeactivateAsync()
        {
            // Provides Opportunity to perform optional cleanup.
            return Task.CompletedTask;
        }

        private Task TimerCallBack(object data)
        {
            // Code for time callback can be added here.
            return Task.CompletedTask;
        }
    }
}
