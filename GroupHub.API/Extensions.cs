using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GroupHub.Core.Common.Enums;
using GroupHub.Core.Services;
using GroupHub.Infra.Configurations;
using GroupHub.Infra.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json; 

namespace GroupHub.API
{
    public static class Extensions
    {
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

        }

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
