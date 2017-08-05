using System;
using System.Configuration;

namespace GroupHub.Configuration
{
    public sealed class SecuritySection : SectionBase
    {
        public static string SectionName { get { return string.Format("{0}/{1}", SectionBase.GroupName, "Security"); } }

        private static SecuritySection settings = ConfigurationManager.GetSection(SectionName) as SecuritySection;


        public static SecuritySection Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("OAuthAccessTokenIssuer", IsRequired = true)]
        public string OAuthAccessTokenIssuer
        {
            get
            {
                return (string)this["OAuthAccessTokenIssuer"];
            }
            set
            {
                this["OAuthAccessTokenIssuer"] = value;
            }
        }

        [ConfigurationProperty("OAuthAccessTokenAudience", IsRequired = true)]
        public string OAuthAccessTokenAudience
        {
            get
            {
                return (string)this["OAuthAccessTokenAudience"];
            }
            set
            {
                this["OAuthAccessTokenAudience"] = value;
            }
        }

        
        [ConfigurationProperty("PrivateCertificate", IsRequired = false)]
        public string PrivateCertificate
        {
            get
            {
                return (string)this["PrivateCertificate"];
            }
            set
            {
                this["PrivateCertificate"] = value;
            }
        }

        [ConfigurationProperty("PublicCertificate", IsRequired = false)]
        public string PublicCertificate
        {
            get
            {
                return (string)this["PublicCertificate"];
            }
            set
            {
                this["PublicCertificate"] = value;
            }
        }

        [ConfigurationProperty("CertificatePassword", IsRequired = false)]
        public string CertificatePassword
        {
            get
            {
                return (string)this["CertificatePassword"];
            }
            set
            {
                this["CertificatePassword"] = value;
            }
        }



    }
}
