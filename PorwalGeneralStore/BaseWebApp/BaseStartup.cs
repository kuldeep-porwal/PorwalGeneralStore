using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace BaseWebApp
{
    public abstract class BaseStartup
    {
        public BaseStartup(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        public readonly ILogger _logger;

        public abstract void ConfigureServices(IServiceCollection services);
        // This method gets called by the runtime. Use this method to add services to the container.
        public void BaseConfigureServices(IServiceCollection services)
        {
            ConfigureSwagger(services);
        }

        public abstract void Configure(IApplicationBuilder app, IHostingEnvironment env);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void BaseConfigure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test Api v1");
            });
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            var SwaggerConfiguration = new Info();
            Configuration.GetSection("SwaggerConfiguration").Bind(SwaggerConfiguration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", SwaggerConfiguration);
            });
            _logger.LogInformation("Swagger Configured Successfully");
        }

        public void AddActionFIlterServices(IServiceCollection services)
        {
            services.AddScoped<LoggingActionFilter>();

            _logger.LogInformation("Filter Configured Successfully");
        }
    }
}