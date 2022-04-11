using Autofac;
using ReserveAqui.Infra.Data.Contexts;
using System.Reflection;

namespace ReserveAqui.Infra.CrossCutting.IoC
{
	public static class AutoFacExtension
    {
		public static void AddAutofacServiceProvider(this ContainerBuilder builder)
		{
			builder.RegistrarContexto();
			builder.RegistrarServices();
			builder.RegistrarRepositorio();
			builder.RegistrarAppServices();
		}

		private static void RegistrarContexto(this ContainerBuilder builder)
		{
			builder.RegisterType<DbMySqlContext>().InstancePerLifetimeScope();
		}

		private static void RegistrarAppServices(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("ReserveAqui.AppService")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}

		private static void RegistrarServices(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("ReserveAqui.Domain")).AsImplementedInterfaces().InstancePerLifetimeScope();		
		}

		private static void RegistrarRepositorio(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("ReserveAqui.Infra.Data")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}
