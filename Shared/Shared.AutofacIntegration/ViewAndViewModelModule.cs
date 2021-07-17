using System.Reflection;
using Autofac;

namespace Shared.AutofacIntegration
{
	/// <summary>
	/// This Module registers all ViewModels and Views in given Assembly
	/// </summary>
	public class ViewAndViewModelModule : Autofac.Module
	{
		private readonly Assembly[] _assemblies;

		public ViewAndViewModelModule(params Assembly[] assemblies)
		{
			_assemblies = assemblies;
		}

		protected override void Load(ContainerBuilder builder)
		{
			foreach (var assembly in _assemblies)
			{
				builder.RegisterAssemblyTypes(assembly)
					.Where(t => t.Name.EndsWith("ViewModel"))
					.AsSelf();

				builder.RegisterAssemblyTypes(assembly)
					.Where(t => t.Name.EndsWith("View"))
					.AsSelf();
			}
		}
	}
}