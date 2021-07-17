namespace Shared.Events.Tests.TestImplementation
{
    using Shared.Events.Api;

    public class TestEventGateway : ITestEventGateway
    {
	    private readonly IEventServiceClient<TestEvent> _serviceClient;

	    public TestEventGateway(IEventServiceClient<TestEvent> serviceClient)
	    {
		    _serviceClient = serviceClient;
	    }

	    public void Send(TestEvent @event)
	    {
		    _serviceClient.Raise(@event);
	    }
    }
    public class TestEventGateway3 : ITestEventGateway3
    {
	    private readonly IEventServiceClient<TestEvent3> _serviceClient;

	    public TestEventGateway3(IEventServiceClient<TestEvent3> serviceClient)
	    {
		    _serviceClient = serviceClient;
	    }

	    public void Send(TestEvent3 @event)
	    {
		    _serviceClient.Raise(@event);
	    }
    }
}