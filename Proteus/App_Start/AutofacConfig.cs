using Autofac;
using Autofac.Integration.Mvc;
using Proteus.DAL;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Proteus
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<ProteusDBEntities>().AsSelf();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") && (t.Namespace.Contains("Services") || t.Namespace.Contains("ServicesMoria")))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}