using domain;
using domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using repository.Implementations;
using System;
using System.Linq;

namespace repository
{
    public static class Injector
    {
        private static string GetConnectionString(string databaseUrl)
        {
            databaseUrl = databaseUrl.Replace("//", "");

            char[] delimiterChars = { '/', ':', '@', '?' };
            string[] strConn = databaseUrl.Split(delimiterChars);
            strConn = strConn.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var User = strConn[1];
            var Password = strConn[2];
            var Server = strConn[3];
            var Database = strConn[5];
            var Port = strConn[4];

            var connectionString = "host=" + Server
                + ";port=" + Port
                + ";database=" + Database
                + ";uid=" + User
                + ";pwd=" + Password
                + ";sslmode=Require;Trust Server Certificate=true;Timeout=1000";

            return connectionString;
        }

        public static void InjectDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            databaseUrl = string.IsNullOrEmpty(databaseUrl) ? configuration.GetValue<string>("DATABASE_URL") : databaseUrl;
            var connectionString = GetConnectionString(databaseUrl);

            services.AddDbContext<CursosCastGroupContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("app")), ServiceLifetime.Transient);
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<Curso>, GenericRepository<Curso>>();
            services.AddTransient<IGenericRepository<Categoria>, GenericRepository<Categoria>>();
        }
    }
}
