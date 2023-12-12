namespace PruebaIngresoBibliotecario.Api.Configurations
{
    using Microsoft.Extensions.DependencyInjection;
    using PruebaIngresoBibliotecario.Business;
    using PruebaIngresoBibliotecario.Infrastructure;

    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddBusiness();

            return services;
        }
    }
}
