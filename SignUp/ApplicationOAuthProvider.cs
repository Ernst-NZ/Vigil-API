﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using SignUp.Models;

namespace SignUp
{

  public class ApplicationOAuthProvider2 : OAuthAuthorizationServerProvider
  {

    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      context.Validated();
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
      var manager = new UserManager<ApplicationUser>(userStore);
      var user = await manager.FindAsync(context.UserName, context.Password);
      if (user == null)
      {
        context.SetError("invalid_grant", "The email address or password is incorrect.");
        return;
      }
      if (user != null)
      {
        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim("Username", user.UserName));
        identity.AddClaim(new Claim("Email", user.Email));
        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
        context.Validated(identity);
      }
      else
      {
        return;
      }
    }
  }
}