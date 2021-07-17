using System.Collections.Generic;

namespace Shared.Events.Api
{
	public interface ISubscriberList
	{
		List<ISubscriber> Subscribers { get; }
	}
}