using System;

namespace Shared.Events.Api
{
    public abstract class Subscriber<TEvent>: ISubscriber where TEvent:IEvent
    {
	    protected abstract void Handle(TEvent @event);

        public void  Handle(IEvent @event)
        {
	        if (@event is TEvent tEvent)
	        {
		        Handle(tEvent);
	        }
        }

        public Type EventType()
        {
	        return typeof(TEvent);
        }
    }
}