using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Web.Api.oData.Models;

namespace Web.Api.oData
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
            services.AddControllers(mvcOptions => 
                mvcOptions.EnableEndpointRouting = false);

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().Filter().OrderBy().Count().MaxTop(null);
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });

   
        }
        /// <summary>
        /// Define the model information.
        /// </summary>
        /// <returns></returns>
        IEdmModel GetEdmModel()
        {
            // Define the metadata information.
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Storage>("Storages")
                .EntityType.HasKey(i => i.Uuid);

            // define function to get all storages.
            var fnAllStorages = odataBuilder.Function("Storages");
            fnAllStorages.ReturnsCollectionFromEntitySet
                <Storage>("Storages");

            return odataBuilder.GetEdmModel();
        }
    }
}
