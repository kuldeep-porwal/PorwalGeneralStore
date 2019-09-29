﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseWebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PorwalGeneralStore.BusinessLayer.Implementation.Products;
using PorwalGeneralStore.BusinessLayer.Interface.Products;
using PorwalGeneralStore.DataAccessLayer.Implementation.Products;
using PorwalGeneralStore.DataAccessLayer.Interface.Products;
using PorwalGeneralStore.EdmxModel;
using Serilog;

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

            services.AddDbContext<PorwalGeneralStoreContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

            services.AddScoped<IProductBiz, ProductBiz>();
            services.AddScoped<IProductLayer, ProductLayer>();
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