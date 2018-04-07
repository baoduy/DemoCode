using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace TestApi
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
            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters();

            //Enable Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test Api",
                    Description = "Test Api with Identity Server 4",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Hoang Bao Duy",
                        Email = "drunkcoding@outlook.com",
                        Url = "https://drunkcoding.net"
                    }
                });

                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "Authorization header using the Bearer scheme",
                //    Name = "Authorization",
                //    In = "header",
                //});

                //c.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
            });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:1000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "testapi";
                });

            //Enable CROS for React
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");

            app.UseAuthentication();

            app.UseMvc();

            //Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));
        }
    }
}
