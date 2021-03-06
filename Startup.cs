﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using productionorderservice.Data;
using productionorderservice.Services;
using productionorderservice.Services.Interfaces;
using securityfilter.Services;
using securityfilter.Services.Interfaces;

namespace productionorderservice {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            services.AddSingleton<IConfiguration> (Configuration);
            services.AddTransient<IEncryptService, EncryptService> ();

            if (!String.IsNullOrEmpty (Configuration["KeyFolder"]))
                services.AddDataProtection ()
                .SetApplicationName ("Lorien")
                .PersistKeysToFileSystem (new DirectoryInfo (Configuration["KeyFolder"]));

            services.AddSingleton<IRecipePhaseService, RecipePhaseService> ();
            services.AddSingleton<IRecipeService, RecipeService> ();
            services.AddSingleton<IThingService, ThingService> ();
            services.AddSingleton<IThingGroupService, ThingGroupService> ();
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseNpgsql (Configuration.GetConnectionString ("ProductionOrderDb")));
            services.AddTransient<IStateConfigurationService, StateConfigurationService> ();
            services.AddTransient<IProductionOrderTypeService, ProductionOrderTypeService> ();
            services.AddTransient<IProductionOrderService, ProductionOrderService> ();
            services.AddTransient<IStateManagementService, StateManagementService> ();
            services.AddTransient<IHistStateService, HistStatesService> ();

            services.AddTransient<IAssociateProductionOrderService, AssociateProductionOrderService> ();

            services.AddResponseCaching ();
            services.AddMvc ((options) => {
                options.CacheProfiles.Add ("productionordercache", new CacheProfile () {
                    Duration = Convert.ToInt32 (Configuration["CacheDuration"]),
                        Location = ResponseCacheLocation.Any
                });
            }).AddJsonOptions (options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseResponseCaching ();
            app.UseCors ("CorsPolicy");
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();
        }
    }
}