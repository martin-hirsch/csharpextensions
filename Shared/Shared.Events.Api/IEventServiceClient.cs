namespace Shared.Events.Api
{
    public interface IEventServiceClient<TEvent> where TEvent : IEvent
    {
        void Raise(TEvent @event);

    }
}