using IdentityServer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
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
            //Support UI
            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Configs.GetApiResources())
                .AddInMemoryClients(Configs.GetClients())
                .AddTestUsers(Configs.GetUsers())
                .AddInMemoryIdentityResources(Configs.GetIdentityResources())
                //Custom User profile service for Window Accounts
                .AddProfileService<WindowAccountProfileService>();

            //Enable Window Authentication. REMEMBER to enable the Window Authentication of IIS as well.
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            //Support UI
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
