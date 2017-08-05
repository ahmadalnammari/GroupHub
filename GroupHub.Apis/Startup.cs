using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using GroupHub.Apis;
using GroupHub.Configuration;
using GroupHub.Core;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(GroupHub.Apis.Startup))]
namespace GroupHub.Apis
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.Use<GlobalExceptionMiddleware>();
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureOAuth(app);

            //this method will be called on every request
            //app.UseClaimsTransformation(ClaimsTransformation);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        //private async Task<ClaimsPrincipal> ClaimsTransformation(ClaimsPrincipal incoming)
        //{

        //    if (!incoming.Identity.Name.Contains("TAHALUF"))
        //    {
        //        return incoming;
        //    }
            
        //    var name = incoming.Identity.Name;

        //    //call database to get claims
            
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, name),
        //        new Claim(ClaimTypes.Role, "API"),
        //    };

        //    var windowsAuthClaims = new ClaimsIdentity("Windows");
        //    windowsAuthClaims.AddClaims(claims);

        //    return new ClaimsPrincipal(windowsAuthClaims);
        //}

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider(),
                AccessTokenFormat = new CustomJwtFormat(SecuritySection.Settings.OAuthAccessTokenIssuer)
            };

            // Token Generation

            //get private key
            X509Certificate2 cert = new X509Certificate2(Path.Combine(Helper.AssemblyDirectory, SecuritySection.Settings.PublicCertificate), SecuritySection.Settings.CertificatePassword);

     
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { SecuritySection.Settings.OAuthAccessTokenAudience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new X509CertificateSecurityTokenProvider(SecuritySection.Settings.OAuthAccessTokenIssuer, cert)
                    },

            });

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            

        }




    }
}