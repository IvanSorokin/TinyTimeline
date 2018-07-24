using System.Globalization;
using DataAccess.Concrete.Mappers;
using DataAccess.Concrete.Repositories;
using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using DataAccess.Interfaces.Repositories;
using Domain.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TinyTimeline.Helpers;
using TinyTimeline.ModelBuilding;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices();

            services.AddAuthorization(options =>
                                      {
                                          options.AddPolicy(PolicyNames.OnlyAdmin,
                                                            policy =>
                                                            {
                                                                policy.Requirements.Add(new UserRoleRequirement(new[] {UserRole.Administrator}));
                                                            });
                                          options.AddPolicy(PolicyNames.OnlyAuthUser,
                                                            policy =>
                                                            {
                                                                policy.Requirements.Add(new UserRoleRequirement(new[]
                                                                                                           {
                                                                                                               UserRole.Administrator,
                                                                                                               UserRole.Participant
                                                                                                           }));
                                                            });
                                      });
            

            services.AddSingleton<IAuthorizationHandler, UserRoleRequirementHandler>()
                    .AddSingleton(GetCollection<SessionDocument>("sessions"))
                    .AddSingleton<IAuthTokenHelper, AuthTokenHelper>()
                    .AddSingleton<ITimelineEventModelBuilder, TimelineEventModelBuilder>()
                    .AddSingleton<ISessionsRepository, SessionsRepository>()
                    .AddSingleton<ISessionModelBuilder, SessionModelBuilder>()
                    .AddTransient<ITwoWayMapper<TimelineEventDocument, TimelineEvent>, TimelineEventsMapper>()
                    .AddTransient<ITwoWayMapper<SessionDocument, Session>, SessionMapper>()
                    .AddTransient<ITwoWayMapper<ReviewDocument, Review>, ReviewMapper>()
                    .AddHttpContextAccessor();

        }
        
        private IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var connectionString = Configuration["MongoConnection:ConnectionString"];
            var dbString = Configuration["MongoConnection:Database"];
            return new MongoClient(connectionString).GetDatabase(dbString).GetCollection<T>(collectionName);
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