using efcore2_webapi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FluentModelBuilder.Extensions;
using efcore2_webapi.Repository.Mappings;
using efcore2_webapi.Domain.Entities;
using FluentModelBuilder.Relational.Conventions;
using FluentModelBuilder.Conventions;
using efcore2_webapi.Repository.Infrastructure;
using efcore2_webapi.Infrastructure;
using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.AppServices;
using efcore2_webapi.Data;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace efcore2_webapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(opt =>
                            {
                                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                            });

            AddModelBuilderContext(services);

            AddAppFrameworkServices(services);
            
            AddRepositoryServices(services);

            AddAppServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            var personJson = File.ReadAllText(@"./Data/UserData.json");
            app.SeedPersonData(personJson);

            var todoJson = File.ReadAllText(@"./Data/TodoItemData.json");
            app.SeedTodoData(todoJson);
        }


        private void AddModelBuilderContext(IServiceCollection services)
        {
            var config = Configuration["Data:PostgresConnection:ConnectionString"];

            services.AddDbContext<ModelBuilderContext>(options => options.UseNpgsql(config)
                    .Configure(fluently =>
                                fluently.Using(new EntityAutoConfiguration())
                                        .AddEntitiesFromAssemblyOf<TodoItem>()
                                        .UseConvention<PluralizingTableNameGeneratingConvention>()
                                        // .UseConvention<IgnoreReadOnlyPropertiesConvention>()
                                        //.AddAlterationsFromAssemblyOf<TodoItem>()
                                        .UseOverridesFromAssemblyOf<TodoItem>()
                            ));
        }

        private void AddAppFrameworkServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IDbContextManager), typeof(DbContextManager));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

        private void AddRepositoryServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IUserRepository), typeof(UserRepository));
            services.AddSingleton(typeof(ITodoRepository), typeof(TodoRepository));
            services.AddSingleton(typeof(ICommentRepository), typeof(CommentRepository));
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ICommentAppService), typeof(CommentAppService));
            services.AddSingleton(typeof(IUserAppService), typeof(UserAppService));
            services.AddSingleton(typeof(ITodoAppService), typeof(TodoAppService));
        }
    }
}
