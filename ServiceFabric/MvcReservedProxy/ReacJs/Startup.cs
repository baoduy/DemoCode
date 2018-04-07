using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ReacJs
{
    public class Startup
    {
        //Enable Reserved Proxy - Add ReservedName, the name can be found in the Service Fabric Explorer
        private const string ServiceNameUrl = "MvcReservedProxy/ReacJs";
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

            //Enable Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ReacJs Api",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Email = "drunkcoding@outlook.net",
                        Name = "DrunkCoding",
                        Url = "http://drunkcoding.net"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#else
            app.UseExceptionHandler("/Home/Error");
#endif
            //Enable Reserved Proxy - handle transform
            app.Use((context, next) =>
            {
                //Apply the fabric URL if accessing by the Service Fabric Reverse Proxy
                if (context.Request.Headers.TryGetValue("X-Forwarded-Host", out var _))
                    context.Request.PathBase = _reservedProxyUrl;

                return next();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //Enable Reserved Proxy - changes "/swagger/v1/swagger.json" to "v1/swagger.json"
                c.SwaggerEndpoint("v1/swagger.json", "ReacJs Api V1");
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
