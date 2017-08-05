
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using GroupHub.Configuration;
using GroupHub.Core;

namespace GroupHub.Apis
{

    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }


            X509Certificate2 cert = new X509Certificate2(Path.Combine(Helper.AssemblyDirectory, SecuritySection.Settings.PrivateCertificate), SecuritySection.Settings.CertificatePassword, X509KeyStorageFlags.MachineKeySet);


            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, SecuritySection.Settings.OAuthAccessTokenAudience, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, new X509SigningCredentials(cert));

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }


}