using GroupHub.Configuration;
using System.Configuration;

namespace Tahaluf.ElasticSearch.Configuration
{
    public static class SectionHelper
    {
        public static T GetSection<T>(string sectionName) where T : SectionBase
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }
    }
}
