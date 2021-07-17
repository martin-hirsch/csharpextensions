using System;

namespace Shared.Events.Api
{
	public interface ISubscriber
	{
		void Handle(IEvent @event);
		Type EventType();
	}
}