using System;
using System.Globalization;
using DataAccess;
using Domain.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using TinyTimeline.Policies;

namespace TinyTimeline
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices();

            services.AddAuthorization(options =>
                                      {
                                          options.AddPolicy(PolicyNames.OnlyAdmin,
                                                            p =>
                                                            {
                                                                p.Requirements.Add(new UserRoleRequirement(new[] {UserRole.Administrator}));
                                                            });
                                          options.AddPolicy(PolicyNames.OnlyAuthUser,
                                                            p =>
                                                            {
                                                                p.Requirements.Add(new UserRoleRequirement(new[]
                                                                                                           {
                                                                                                               UserRole.Administrator,
                                                                                                               UserRole.Participant
                                                                                                           }));
                                                            });
                                      });
            

            services.AddSingleton<IAuthorizationHandler, UserRoleRequirementHandler>()
                    .AddHttpContextAccessor();

            return ConfigureIoC(services);
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var registry = new Registry();
            registry.IncludeRegistry<DataAccessRegistry>();

            var container = new Container(registry);

            container.Configure(config =>
                                {
                                    config.Scan(_ =>
                                                {
                                                    _.AssemblyContainingType(typeof(Startup));
                                                    _.AssemblyContainingType(typeof(DataAccessRegistry));
                                                    _.WithDefaultConventions();
                                                });
                                    config.Populate(services);
                                });

            return container.GetInstance<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[] {new CultureInfo("en-GB")};
            app.UseRequestLocalization(new RequestLocalizationOptions
                                       {
                                           DefaultRequestCulture = new RequestCulture("en-GB"),
                                           SupportedCultures = supportedCultures,
                                           SupportedUICultures = supportedCultures
                                       });
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
                       {
                           routes.MapRoute(
                                           "default",
                                           "{controller=Home}/{action=Index}/{id?}");
                       });
        }
    }
}