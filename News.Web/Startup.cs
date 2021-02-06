using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using News.Core;
using News.Core.Services;
using News.Core.Services.Interfaces;
using News.DataLayer.Context;
using News.DataLayer.Entities;

namespace News.Web
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        public Startup(IConfiguration Configuration)
        {
            this._Configuration = Configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding Mvc And Desable EndPoint Routing 
            
            services.AddMvc(options => options.EnableEndpointRouting = false);

            #region Context
            services.AddDbContext<NewsContext>(options =>
            {
                options.UseSqlServer(_Configuration.GetConnectionString("NewsConnectionString"));
            });
            #endregion

            #region IoC
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            #endregion

            #region Identity Config
            
            services.AddIdentity<Users, IdentityRole>(opt =>
            {

                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;

                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@ض ص ث ق ف غ ع ه خ ح ج چ ش س ی ب ل ا ت ن م و د ذ ر ز ط ظ  0 1 2 3 5 6 7 8 9 4 ";
            })
            .AddEntityFrameworkStores<NewsContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<TranslateIdentity>();
            #endregion
            #region Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/AccessDenied";
                options.LoginPath = "/Login";
                options.Cookie.HttpOnly = true;
                options.LogoutPath = "/Logout";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}");
                endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
            });
        }
    }
}
