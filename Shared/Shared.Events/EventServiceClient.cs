using Shared.Events.Api;
using Xamarin.Forms;

namespace Shared.Events
{
    public class EventServiceClient <TEvent> : IEventServiceClient<TEvent> where TEvent: IEvent
    {
        public void Raise(TEvent @event)
        {
            MessagingCenter.Send(this,"", @event);
        }
    }
}