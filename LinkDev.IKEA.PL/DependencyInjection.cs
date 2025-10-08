using System.Reflection;

namespace LinkDev.IKEA.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
