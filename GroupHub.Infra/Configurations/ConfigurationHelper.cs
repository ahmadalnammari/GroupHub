using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupHub.Infra.Configurations
{
  public  static class ConfigurationHelper
    {
        public static IConfiguration configuration;
        public static string GetValByKey(string key)
        {
            return configuration[key];
        }
    }
}
