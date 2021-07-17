using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shared.Events.Api;
using Xamarin.Forms;

namespace Shared.Events
{
	public class SubscriberList : ISubscriberList,IDisposable
	{
		public List<ISubscriber> Subscribers { get; }

		public SubscriberList(IEnumerable<ISubscriber> subscribers)
		{
			Subscribers = subscribers.ToList();

			foreach (var subscriber in Subscribers)
			{
				Register(subscriber);
			}
				
		}
		private void Unregister(ISubscriber subscriber)
		{
			var unsubscribeMethod = GetMethodFromMessagingCenter(nameof(MessagingCenter.Unsubscribe)).MakeEventServiceClientFor(subscriber);

			unsubscribeMethod.Invoke(null, new object[] {subscriber, ""});
		}


		private void Register(ISubscriber subscriber)
		{
			var subscribeMethod = GetMethodFromMessagingCenter(nameof(MessagingCenter.Subscribe)).MakeEventServiceClientFor(subscriber);

			subscribeMethod.Invoke(null, new object[] {subscriber, "", CreateLamdaExpressionWithSubscribeCall(subscriber).Invoke(this, new object[]{subscriber}), null});
		}

		private MethodInfo CreateLamdaExpressionWithSubscribeCall(ISubscriber subscriber)
		{
			return typeof(SubscriberList).GetMethod(name: nameof(SubscriberList.CreateAction)).MakeGenericMethod(subscriber.EventType());
		}

		public Action<EventServiceClient<TEvent>, TEvent> CreateAction<TEvent>(ISubscriber subscriber) where TEvent : IEvent
		{
			Action<EventServiceClient<TEvent>, TEvent> action = (sender, @event) => subscriber.Handle(@event);
			return action;
		}
		private static MethodInfo GetMethodFromMessagingCenter(string methodName)
		{
			var method = typeof(MessagingCenter)
				.GetMethods()
				.Where(x => x.Name == methodName)
				.FirstOrDefault(
					mi => mi.GetGenericArguments().Count() == 2);
			return method;
		}





		private void ReleaseUnmanagedResources()
		{
			foreach (var subscriber in Subscribers.Where(subscriber => subscriber != null))
				Unregister(subscriber);
			Subscribers.Clear();
		}


		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}

		~SubscriberList()
		{
			ReleaseUnmanagedResources();
		}

	}

	public static class TypeExtensions
	{
		public static Type GetEventType(this Type subscriberType)
		{

			if (ImplementsGenericSubscriber(subscriberType.BaseType))
				return subscriberType.BaseType.GetGenericArguments()[0];

			throw new NotSupportedException(
				$"This method should only be used, if you checked, that the given type implements {typeof(Subscriber<>)}.");
		}
		private static bool ImplementsGenericSubscriber(Type type)
		{
			return type.IsGenericType &&
			       type.GetGenericTypeDefinition()
			       == typeof(Subscriber<>);
		}
	}

	public static class MethodInfoExtensions
	{
		public static MethodInfo MakeEventServiceClientFor(this MethodInfo method, ISubscriber subscriber)
		{
			return method.MakeGenericMethod(typeof(EventServiceClient<>).MakeGenericType(subscriber.EventType()), subscriber.EventType());
		}
	}
}