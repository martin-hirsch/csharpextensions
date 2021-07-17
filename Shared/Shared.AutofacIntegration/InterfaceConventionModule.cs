using System.Linq;
using System.Reflection;
using Autofac;

namespace Shared.AutofacIntegration
{
	/// <summary>
	/// This Module Registers all Classes in given Assemblies implementing an Interface which has the same name just with an additional I in front.
	/// e.g class Example:IExample will be registered a Service IExample
	/// </summary>
	public class InterfaceConventionModule : Autofac.Module
	{
		private readonly Assembly[] assemblies;

		public InterfaceConventionModule(params Assembly[] assemblies)
		{
			this.assemblies = assemblies;
		}

		protected override void Load(ContainerBuilder builder)
		{
			foreach (var assembly in assemblies)
			{
				var types = assembly.GetTypes().Where(x => x.GetInterfaces().Any(y => y.Name == "I" + x.Name));
				foreach (var type in types)
				{
					builder.RegisterType(type).As(type.GetInterface("I"+type.Name));
				}
			}
		}
	}
}