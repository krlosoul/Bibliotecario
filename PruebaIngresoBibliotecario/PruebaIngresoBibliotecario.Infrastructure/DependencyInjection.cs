namespace PruebaIngresoBibliotecario.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Infrastructure.DataAccess;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<BibliotecaContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
