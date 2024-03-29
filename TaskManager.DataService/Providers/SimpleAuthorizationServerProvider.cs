﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TaskManager.DataService.Database;
using TaskManager.DataService.Models;

namespace TaskManager.DataService.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            using (var authRepository = new AuthRepository())
            {
                ApplicationUser user = await authRepository.FindUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Неверный логин или пароль");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await authRepository.CreateIdentityAsync(user, context.Options.AuthenticationType);
                List<Claim> roles = oAuthIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                AuthenticationProperties properties = CreateProperties(user.UserName, user.Id, user.FullName, Newtonsoft.Json.JsonConvert.SerializeObject(roles.Select(x => x.Value)));
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
            }
        }

        public static AuthenticationProperties CreateProperties(string login, int userId, string userName, string roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userId", userId.ToString() },
                {"username", userName},
                {"login", login},
                {"userRoles", roles}
            };
            return new AuthenticationProperties(data);
        }
    }
}