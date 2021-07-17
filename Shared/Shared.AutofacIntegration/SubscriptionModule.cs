using System.Linq;
using System.Reflection;
using Autofac;
using Shared.Events;
using Shared.Events.Api;

namespace Shared.AutofacIntegration
{
	/// <summary>
	/// Register all implementations of ISubscriber  in given Assemblies as a ISubscriber.
	/// </summary>
	public class SubscriptionModule : Autofac.Module
	{
		private readonly Assembly[] assemblies;

		public SubscriptionModule(params Assembly[] assemblies)
		{
			this.assemblies = assemblies;
		}

		protected override void Load(ContainerBuilder builder)
		{
			foreach (var assembly in assemblies)
				builder.RegisterAssemblyTypes(assembly)
					.Where(t => t.GetInterfaces().Contains(typeof(ISubscriber)))
					.As<ISubscriber>();

			//Register SubscriberClass
			builder.RegisterType<SubscriberList>().As<ISubscriberList>();
		}
	}
}