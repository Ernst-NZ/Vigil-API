using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;
using System;

namespace SignUp.Models
{
  public class HangfireAthorizationFilter : IDashboardAuthorizationFilter
  {
    public bool Authorize([NotNull] DashboardContext context)
    {
      // In case you need an OWIN context, use the next line, `OwinContext` class
      // is the part of the `Microsoft.Owin` package.
      //  var owinContext = new OwinContext(context.GetOwinEnvironment());

      // Allow all authenticated users to see the Dashboard (potentially dangerous).
      //return owinContext.Authentication.User.Identity.IsAuthenticated;
      return true;
    }
  }
}