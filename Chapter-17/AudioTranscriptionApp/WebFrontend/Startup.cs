using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;

namespace WebFrontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
           .AddAzureAd(options =>
           {
               //Configuration.Bind("AzureAd", options)
               options.CallbackPath = "/dummy";
               //options.Instance = "https://login.microsoftonline.com/";
               //options.Domain = "londonorg.onmicrosoft.com";
               //options.TenantId = "9f2792d0-7b20-4549-8bb2-129413404627";
               //options.ClientId = "70f73b7d-f0b7-429c-b153-f1efb1fd595a";
               options.Instance = "https://login.microsoftonline.com/";
               options.Domain = "microsoft.com";
               options.TenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47";
               options.ClientId = "3fd82d20-6bd6-46c9-9f35-945c80be055b";
               //options.ClientId = "4b68cbf0-9911-43e5-963b-7ea7b04b5652";
           }
           )

           .AddCookie();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            //    {
            //        HotModuleReplacement = true
            //    });
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
            
        }
    }
}
