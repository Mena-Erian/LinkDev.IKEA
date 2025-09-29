using LinkDev.IKEA.DAL.Contracts;

namespace LinkDev.IKEA.PL
{
    public static class InitializerExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            
            var services = scope.ServiceProvider;
            var dbInitializer = services.GetRequiredService<IDbInitializer>(); // Ask Explicitly for the serivce

            dbInitializer.Initialize();
            dbInitializer.SeedData();
        }
    }
}
