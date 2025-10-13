using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace LinkDev.IKEA.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddIdentity<ApplicationUser, IdentityRole>();//AddIdentity system configuration
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;// Default
                options.Password.RequireNonAlphanumeric = true; //$"*..

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1; // P@ssword

                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = ;

                options.Lockout.AllowedForNewUsers = true; // lock account
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5; // Max number for trying to login with wrong password

                //options.SignIn.RequireConfirmedAccount = true;
                //options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedEmail = true;

            })
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            /// services.AddAuthentication();
            /// services.AddAuthentication("Cookies");
            /// services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            ///     .AddCookie(options =>
            ///     {
            ///         options.LoginPath = "/Account/SignIn";
            ///         options.AccessDeniedPath = "/Account/SignUp";
            ///         options.LogoutPath = "/Account/LogOut";
            ///         options.LoginPath = "/Account/SignIn";
            ///         options.ExpireTimeSpan = TimeSpan.FromDays(15);
            ///         options.SlidingExpiration = true;
            ///     }); // Called internally in AddIdentity
            /// 
            /// services.AddAuthentication(options =>
            /// {
            ///     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Cookies
            ///     options.DefaultChallengeScheme = "Beaer"; // JWT Bearer Token Authentication Scheme
            ///     //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            /// 
            /// });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Account/SignUp";
                options.LogoutPath = "/Account/LogOut";
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
                options.SlidingExpiration = true;
            });

            //services.AddAuthorization();


            return services;
        }
    }
}
