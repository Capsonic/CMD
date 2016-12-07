using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using CMD.Auth;
using Ninject.Web.Common.OwinHost;
using Ninject;
using System.Reflection;
using Ninject.Web.WebApi.OwinHost;
using CMD.App_Start;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(CMD.Startup))]

namespace CMD
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            HttpConfiguration config = new HttpConfiguration();
            
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            
            //app.UseWebApi(config);
            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel).UseNinjectWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/api/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = new SimpleAuthorizationServerProvider()
            //};

            ////Token Generation
            //app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Wire token validation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:61521",

                //ValidationMode = ValidationMode.ValidationEndpoint,

                // For access to the introspection endpoint
                ClientId = "cmd",
                ClientSecret = "api-secret",

                RequiredScopes = new[] { "api" }
            });

        }
    }
}
