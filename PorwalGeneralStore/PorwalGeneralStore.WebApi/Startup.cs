using BaseWebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using PorwalGeneralStore.BusinessLayer.Implementation.Products;
using PorwalGeneralStore.BusinessLayer.Interface.Products;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.EdmxModel;
using PorwalGeneralStore.Global.ExtensionMethods;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using Serilog;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Implementation;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Implementation;
using PorwalGeneralStore.BusinessLayer.Interface.Orders;
using PorwalGeneralStore.BusinessLayer.Implementation.Orders;
using PorwalGeneralStore.DataAccessLayer.Interface.Orders;
using PorwalGeneralStore.DataAccessLayer.Implementation.Orders;

namespace PorwalGeneralStore.WebApi
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger) : base(configuration, logger)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("Configuring Services Started");
            BaseConfigureServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            _logger.LogInformation("Configuring Services Finished");
            _ = services.AddSingleton(Configuration.BindAndReturn<JwtConfiguration>("JwtConfiguration"));
            _ = services.AddSingleton(Configuration.BindAndReturn<HttpWebRequestConfiguration>("HttpWebRequestConfiguration"));
            _ = services.AddSingleton(Configuration.BindAndReturn<Msg91BulkSmsServiceConfiguration>("HttpWebRequestConfiguration"));
            services.AddDbContext<PorwalGeneralStoreContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

            services.AddScoped<IProductBiz, ProductBiz>();
            services.AddScoped<IProductLayer, ProductLayer>();

            services.AddScoped<IUserBiz, UserBiz>();
            services.AddScoped<IUserLayer, UserLayer>();

            services.AddScoped<IOrderBiz, OrderBiz>();
            services.AddScoped<IOrderLayer, OrderLayer>();

            services.AddScoped<IJwtBuilder, JwtBuilder>();

            services.AddScoped<IMsg91, Msg91>();
            services.AddScoped<IHttpWebRequestHandler, HttpWebRequestHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            BaseConfigure(app, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
