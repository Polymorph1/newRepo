using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using PerformanceEvaluationPortal.WebAPI.Models;
using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DAL;
using System.Data.Entity;

namespace PerformanceEvaluationPortal.WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);
          
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        
           
            IList<string> roles = await userManager.GetRolesAsync(user.Id);

            bool isManager = ctx.Users
                .Include(x => x.UsersIManage)
                .Where(x => x.Id == user.Id)
                .FirstOrDefault()
                .UsersIManage.Count() > 0;          

            bool isReviewer = ctx.Users
                .Include(x => x.UsersIWriteReviewFor)
                .Where(x => x.Id == user.Id)
                .FirstOrDefault()
                .UsersIWriteReviewFor.Count() > 0;

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.FirstName + " " + user.LastName,
                                                            user.UserName,
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(roles), 
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(isManager), 
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(isReviewer)
                                                           );

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string loggedUser,string userName, string roles = null, string isManager = null, string isReviewer = null)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "roles", roles},
                { "isManager", isManager } ,
                { "isReviewer", isReviewer },
                {"loggedUser",loggedUser }
            };

            return new AuthenticationProperties(data);
        }
    }
}