using System.Collections.Generic;
using System.Linq;
using Autofac;
using NUnit.Framework;
using Shared.AutofacIntegration;
using Shared.Events.Api;
using Shared.Events.Tests.TestImplementation;

namespace Shared.Events.Tests
{
    public class BasicEventTests
    {
        [Test]
        public void AutofacConfiguration_Register_TestImplementation_Via_Modules_Works()
        {
            //Register Testimplentation
            var builder = new ContainerBuilder();
            builder.RegisterModule(new EventModule(typeof(TestEvent).Assembly));
            builder.RegisterModule(new SubscriptionModule(typeof(TestEvent).Assembly));

            builder.RegisterType<TestEventGateway>().AsSelf();
            var container = builder.Build();
            //Resolve needed instances
            var subscriber = container.Resolve<ISubscriberList>();
            var sut = container.Resolve<TestEventGateway>();

            sut.Send(new TestEvent());

            var direktImplementation = subscriber.Subscribers.OfType<DirectInterfaceImplementation>().Single();
            Assert.That(direktImplementation.NumberOfEventsRecieved, Is.EqualTo(1));
        }

        [Test]
        public void Direct_Implementation_Does_Not_React_On_Wrong_Event()
        {
            var testEventGateway = new TestEventGateway3(new EventServiceClient<TestEvent3>());
            var subscriber = new DirectInterfaceImplementation();

            var container = new SubscriberList(new List<ISubscriber> { subscriber });

            var service = new TestService3(testEventGateway);

            service.Method();

            Assert.That(subscriber.NumberOfEventsRecieved, Is.EqualTo(0));
        }

        [Test]
        public void Implementing_ISubscriber_Directly_Should_Work_As_Expected()
        {
            var testEventGateway = new TestEventGateway(new EventServiceClient<TestEvent>());
            var subscriber = new DirectInterfaceImplementation();

            var container = new SubscriberList(new List<ISubscriber> { subscriber });

            var service = new TestService(testEventGateway);

            service.Method();

            Assert.That(subscriber.NumberOfEventsRecieved, Is.EqualTo(1));
        }

        [Test]
        public void Register_EventHandler_Respects_SubscriberInstances()
        {
            var testEventGateway = new TestEventGateway(new EventServiceClient<TestEvent>());
            var subscriber1 = new TestEventSubscriber();
            var subscriber2 = new TestEventSubscriber();

            var container = new SubscriberList(new List<Subscriber<TestEvent>> { subscriber1, subscriber2 });


            var service = new TestService(testEventGateway);

            service.Method();

            Assert.That(subscriber1.NumberOfEventsRecieved, Is.EqualTo(1));
        }

        [Test]
        public void Register_EventHandler_Twice_Calls_Subscriber_Twice()
        {
            var testEventGateway = new TestEventGateway(new EventServiceClient<TestEvent>());
            var subscriber1 = new TestEventSubscriber();

            var container = new SubscriberList(new List<ISubscriber> { subscriber1, subscriber1 });

            var service = new TestService(testEventGateway);

            service.Method();

            Assert.That(subscriber1.NumberOfEventsRecieved, Is.EqualTo(2));
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEvent_Fired_Gets_Forwarded_To_Subscriber()
        {
            var testEventGateway = new TestEventGateway(new EventServiceClient<TestEvent>());
            var subscriber = new TestEventSubscriber();

            var container = new SubscriberList(new List<ISubscriber> { subscriber });

            var service = new TestService(testEventGateway);

            service.Method();

            Assert.That(subscriber.NumberOfEventsRecieved, Is.EqualTo(1));
        }

        [Test]
        public void TestEvent2_Fired_Gets_Not_Forwarded_To_Non_Subscriber()
        {
            var testEventGateway = new TestEventGateway(new EventServiceClient<TestEvent>());
            var subscriber1 = new TestEventSubscriber();
            var subscriber2 = new TestEventSubscriber2();

            var container = new SubscriberList(new List<ISubscriber> { subscriber1, subscriber2 });

            var service = new TestService(testEventGateway);

            service.Method();

            Assert.That(subscriber1.NumberOfEventsRecieved, Is.EqualTo(1));
            Assert.That(subscriber2.NumberOfEventsRecieved, Is.EqualTo(0));
        }
    }
}