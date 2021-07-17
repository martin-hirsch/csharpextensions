using System;

namespace Shared.Events.Tests.TestImplementation
{
    using Shared.Events.Api;

    public class TestEventSubscriber : Subscriber<TestEvent>
    {
        public int NumberOfEventsRecieved;

        protected override void Handle(TestEvent @event)
        {
            NumberOfEventsRecieved++;
        }
    }

    public class TestEventSubscriber2 : Subscriber<TestEvent2>
    {
        public int NumberOfEventsRecieved;

        protected override void Handle(TestEvent2 @event)
        {
            NumberOfEventsRecieved++;
        }
    }

    public class DirectInterfaceImplementation : ISubscriber
    {
	    public int NumberOfEventsRecieved;

	    public void Handle(IEvent @event)
	    {
		    NumberOfEventsRecieved++;
	    }

	    public Type EventType()
	    {
		    return typeof(TestEvent);
	    }
    }
}