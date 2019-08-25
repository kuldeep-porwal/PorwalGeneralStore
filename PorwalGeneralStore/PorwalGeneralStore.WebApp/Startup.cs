using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PorwalGeneralStore.BusinessLayer.Implementation;
using PorwalGeneralStore.BusinessLayer.Interface;
using PorwalGeneralStore.DataAccessLayer.Implementation;
using PorwalGeneralStore.DataAccessLayer.Interface;
using PorwalGeneralStore.EdmxModel;
using DBLayer = PorwalGeneralStore.DataAccessLayer.Implementation;
namespace PorwalGeneralStore.WebApp
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
			services.AddMvc();
			services.AddScoped<ICustomerInfo, DBLayer.CustomerInfo>();
			services.AddScoped<ICustomerInfoBiz, CustomerInfoBiz>();
			services.AddDbContext<PorwalGeneralStoreContext>(
				options =>
						  options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
