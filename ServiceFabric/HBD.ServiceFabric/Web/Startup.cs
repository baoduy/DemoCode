using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public class Startup
    {
          //Enable Reserved Proxy - Add ReservedName, the name can be found in the Service Fabric Explorer
        private const string ServiceNameUrl = "HBD.ServiceFabric/Web";
        //This URL will be use to rendering the resource URL. So all reference resource should be start with ~/
        private readonly string _reservedProxyUrl = $"/{ServiceNameUrl}/";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

              //Enable Reserved Proxy - handle transform
            app.Use((context, next) =>
            {
                //Apply the Preserved Proxy if accessing by the Service Fabric Reserved Proxy
                if (context.Request.Headers.TryGetValue("X-Forwarded-Host", out var _))
                    context.Request.PathBase = _reservedProxyUrl;

                return next();
            });


            app.UseStaticFiles();

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
