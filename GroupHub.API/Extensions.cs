using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GroupHub.Core.Common.Enums;
using GroupHub.Core.Services;
using GroupHub.Infra;
using GroupHub.Infra.Configurations;
using GroupHub.Infra.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;

namespace GroupHub.API
{
    public static class Extensions
    {
        private static string connectionString = "User ID=postgres;Password=0060501;Host=localhost;Port=5432;Database=my_db;Pooling=true;";


        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationHelper.configuration = configuration;
            //Explicit registrations
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISvcStartup, SvcStartup>();


            var assemblyInfrastructureSvc = typeof(Svc).Assembly;

            var registrationsInfrastructureSvc =
                from type in assemblyInfrastructureSvc.GetExportedTypes()
                where type.GetInterfaces().Any(x => x.IsAssignableFrom(typeof(ISvc)))
                where !type.IsAbstract
                select new { Service = type.GetInterfaces().Single(x => x.Name.Contains(type.Name)), Implementation = type };

            foreach (var reg in registrationsInfrastructureSvc)
            {
                services.AddScoped(reg.Service, reg.Implementation);
            }

            //First, configure the SqlConnection and open it
            //This is done for every request/response
            services.AddScoped<DbConnection>((serviceProvider) =>
             {
                 var dbConnection = new NpgsqlConnection(connectionString);
                 dbConnection.Open();
                 return dbConnection;
             });





            //Start a new transaction based on the SqlConnection
            //This is done for every request/response
            services.AddScoped<DbTransaction>((serviceProvider) =>
            {
                var dbConnection = serviceProvider
                    .GetService<DbConnection>();

                return dbConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            });


            //Create DbOptions for the DbContext, use the DbConnection
            //This is done for every request/response
            services.AddScoped<DbContextOptions>((serviceProvider) =>
            {
                var dbConnection = serviceProvider.GetService<DbConnection>();
                return new DbContextOptionsBuilder<GroupHubContext>()
                    .UseNpgsql(dbConnection)
                    .Options;
            });


            //Finally, create the DbContext, using the transaction
            //This is done for every request/response
            services.AddScoped<GroupHubContext>((serviceProvider) =>
            {
                var transaction = serviceProvider.GetService<DbTransaction>();
                var options = serviceProvider.GetService<DbContextOptions>();
                var context = new GroupHubContext(options);
                context.Database.UseTransaction(transaction);
                return context;
            });


            services.AddScoped<IMigrationContext>((serviceProvider) =>
            {


                var ctxOptions = new DbContextOptionsBuilder<GroupHubContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                var context = new GroupHubContext(ctxOptions);
                return context;
            });


            services.AddScoped(typeof(TransactionFilter), typeof(TransactionFilter));

            services
                .AddMvc(setup =>
                {
                    setup.Filters.AddService<TransactionFilter>(1);
                });





            //services.AddEntityFrameworkNpgsql()
            //    .AddDbContext<GroupHubContext>();


        }

        public static IServiceCollection UseMigrations(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddScoped<IMigrationContext>((serviceProvider) =>
            {

      
                var ctxOptions = new DbContextOptionsBuilder<GroupHubContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                var context = new GroupHubContext(ctxOptions);
                return context;
            });

            return serviceCollection;
        }




        //public static IApplicationBuilder MigrateDatabaseUponAppStart(this IApplicationBuilder app, ILogger logger)
        //{
        //    logger.LogTrace("MigrateDatabaseUponAppStart");

        //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //    {
        //        var migrationContext = serviceScope.ServiceProvider.GetRequiredService<IMigrationContext>();
        //        var applicationDbContext = migrationContext as GroupHubContext; //This will always be correct, since we registered it in the method above
        //        var applicationDb = applicationDbContext.Database;
        //        applicationDb.SetCommandTimeout(TimeSpan.FromMinutes(5));
        //        applicationDb.Migrate();
        //    }

        //    logger.LogTrace("MigrateDatabaseUponAppStart suceeded");

        //    return app;
        //}



        public static int GetActiveUserId(this Controller controller) =>
        int.Parse(controller.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);


        public static string GetActiveUserEmail(this Controller controller) =>
          controller.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.Email).Value;

        public static LanguageEnum GetLanguage(this Controller controller) => string.IsNullOrEmpty(controller.HttpContext.Request.Headers["languageId"]) ? LanguageEnum.Arabic :
                (LanguageEnum)int.Parse(controller.HttpContext.Request.Headers["languageId"]);
        
    }

    public static class Serialization
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default(T);
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                return binaryFormatter.Deserialize(memoryStream) as T;
            }
        }


    }


}
