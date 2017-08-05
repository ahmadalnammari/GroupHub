using System.Configuration;

namespace GroupHub.Configuration
{
    public abstract class SectionBase : ConfigurationSection
    {
        protected static string GroupName { get { return ConfigurationManager.AppSettings["GroupConfigName"]; } }
    }
}
