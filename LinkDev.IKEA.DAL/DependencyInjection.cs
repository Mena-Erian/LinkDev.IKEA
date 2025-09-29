using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Data.DbInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            } //, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped
            );

            services.AddScoped<IDbInitializer, DbInitializer>();
            /// services.AddScoped<IDbInitializer, DbInitializer>((serviceProvider) =>
            ///       new DbInitializer(serviceProvider.GetRequiredService<ApplicationDbContext>())
            /// );

            return services;
        }
    }
}
