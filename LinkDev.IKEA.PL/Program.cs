using LinkDev.IKEA.DAL;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        // Entry Point for the application.
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Sevices
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /// builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>();
            /// builder.Services.AddScoped<ApplicationDbContext>();

            /// builder.Services.AddScoped<ApplicationDbContext>(serviceProvieder =>
            /// {
            ///     // var options = serviceProvieder.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            /// 
            ///     var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            /// 
            ///     optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IKEA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            /// 
            ///     return new ApplicationDbContext(optionsBuilder.Options);
            /// });


            /// builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            ///     {
            ///         optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            ///     } //, contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped
            ///     );

            /*
                ServiceLifetime.Singleton // Per Session
                ServiceLifetime.Scoped    // Per Request
                ServiceLifetime.Transient // Per Order
             */

            builder.Services.AddPersistenceServices(builder.Configuration);

            #endregion

            var app = builder.Build();
         
            #region Database Initialization
            
            app.InitializeDatabase();
            
            #endregion
            
            #region Configure Http Request Pipelines
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthorization();
            app.UseAuthorization();

            //app.UseStaticFiles();
            app.MapStaticAssets();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #endregion

            app.Run();
        }
    }
}
