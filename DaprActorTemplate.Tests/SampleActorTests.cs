using System;
using System.Threading;
using Dapr.Actors;
using Dapr.Actors.Runtime;
using DaprActorTemplate.Actor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DaprActorTemplate.Tests
{
    [TestClass]
    public class SampleActorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var actorTypeInformation = ActorTypeInformation.Get(typeof(SampleActor));

            Func<ActorService, ActorId, SampleActor> actorFactory = (service, id) =>
                new SampleActor(service, id, "", ""
                    , new Mock<IRemindableWrapper>().Object
                    , new Mock<IActorStateManager>().Object
                    );
            var actorService = new ActorService(actorTypeInformation, actorFactory);
            var agentCallbackActor = actorFactory.Invoke(actorService, ActorId.CreateRandom());
            //Act
            var apiResponseModel = agentCallbackActor.SaveStateData(null).GetAwaiter().GetResult();
            //Assert
            apiResponseModel.IsError.Should().BeFalse();
            apiResponseModel.Message.Should().Be("CallBackStateData cannot be null.");
        }

        [TestMethod]
        public void TestMethod_SetStateAsync()
        {
            //Arrange
            var callbackStateInfo = new StateData()
            {
                AnotherStringProperty = "Supreme kai",
                FirstName = "Goku",
                StringProperty = "Prince Vegeta"
            };
            var actorTypeInformation = ActorTypeInformation.Get(typeof(SampleActor));
            var actorStateManager = new Mock<IActorStateManager>();
            actorStateManager.Setup(manager => manager.SetStateAsync("callbackstate", callbackStateInfo, CancellationToken.None)).Verifiable();
            Func<ActorService, ActorId, SampleActor> actorFactory = (service, id) => new SampleActor(service, id, "value1", "value1"
                , new Mock<IRemindableWrapper>().Object
                , actorStateManager.Object
            );
            var actorService = new ActorService(actorTypeInformation, actorFactory);
            var agentCallbackActor = actorFactory.Invoke(actorService, ActorId.CreateRandom());
            //Act
            
            var apiResponseModel = agentCallbackActor.SaveStateData(callbackStateInfo).GetAwaiter().GetResult();
            //Assert
            apiResponseModel.IsError.Should().BeTrue();
            apiResponseModel.Message.Should().Be("success");
            actorStateManager.VerifyAll();
        }
    }
}
