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
            #endregion

            var app = builder.Build();

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
