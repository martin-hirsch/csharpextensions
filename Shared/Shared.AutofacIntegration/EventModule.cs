using System.Linq;
using System.Reflection;
using Autofac;
using Shared.Events;
using Shared.Events.Api;
using Module = Autofac.Module;

namespace Shared.AutofacIntegration
{
    public class EventModule : Module
    {
        private readonly Assembly[] assemblies;

        /// <summary>
        ///     Register EventserviceClients for all IEvent Definitions in the given Assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        public EventModule(params Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var type in assemblies.SelectMany(x => x.GetTypes().Where(y => y.IsAssignableTo<IEvent>())))
            {
                var eventServiceClient = typeof(EventServiceClient<>).MakeGenericType(type);
                var iEventServiceClient = typeof(IEventServiceClient<>).MakeGenericType(type);

                builder.RegisterType(eventServiceClient)
                    .As(iEventServiceClient);
            }
        }
    }
}