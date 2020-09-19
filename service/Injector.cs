using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using repository;
using service.Implementations;

namespace service
{
    public static class Injector
    {
        public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.InjectDbContext(configuration);
            services.InjectRepositories();

            services.AddTransient<ICursoService, CursoService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
        }
    }
}
