namespace Shared.Events.Tests.TestImplementation
{
    using Shared.Events.Api;

    public class TestEventGateway2
    {
        private readonly IEventServiceClient<TestEvent2> _serviceClient;

        public TestEventGateway2(IEventServiceClient<TestEvent2> serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public void Send(TestEvent2 @event)
        {
            _serviceClient.Raise(@event);
        }
    }
}