namespace Shared.Events.Tests.TestImplementation
{
	public interface ITestEventGateway
	{
		public void Send(TestEvent @event);
	}
	public interface ITestEventGateway3
	{
		public void Send(TestEvent3 @event);
	}

}