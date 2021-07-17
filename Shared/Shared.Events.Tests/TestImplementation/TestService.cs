namespace Shared.Events.Tests.TestImplementation
{
	public class TestService
	{
		private readonly ITestEventGateway _testEventGateway;

		public TestService(ITestEventGateway testEventGateway)
		{
			_testEventGateway = testEventGateway;
		}

		public void Method()
		{
			var @event = new TestEvent();
			_testEventGateway.Send(@event);
		}
	}
	public class TestService3
	{
		private readonly ITestEventGateway3 _testEventGateway;

		public TestService3(ITestEventGateway3 testEventGateway)
		{
			_testEventGateway = testEventGateway;
		}

		public void Method()
		{
			var @event = new TestEvent3();
			_testEventGateway.Send(@event);
		}
	}
}