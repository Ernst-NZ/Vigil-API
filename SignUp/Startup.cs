using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Hangfire;
using SignUp.Controllers;
using SignUp.Models;

[assembly: OwinStartup(typeof(SignUp.Startup))]

namespace SignUp
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {      // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

      app.UseCors(CorsOptions.AllowAll);

      OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
      {
        TokenEndpointPath = new PathString("/token"),
        Provider = new ApplicationOAuthProvider2(),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
        AllowInsecureHttp = true
      };
      app.UseOAuthAuthorizationServer(option);
      app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

      ConfigureAuth(app);

      //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
      //// app.UseHangfireDashboard();
      //app.UseHangfireDashboard("/Jobs", new DashboardOptions()
      //{
      //  Authorization = new[] { new HangfireAthorizationFilter() }
      //});
      //BackgroundJob.Enqueue(() => Console.WriteLine("HangFire-Startup!"));
      ////RecurringJob.AddOrUpdate<ShopifyGetOrderController>(js => js.GetOrder(),() => "*/15 * * * *");
      //RecurringJob.AddOrUpdate<Startup>(log => log.xxx(), () => "*/10 * * * *");
      //app.UseHangfireServer();
    }

    public void xxx()
    {
      BackgroundJob.Enqueue(() => Console.WriteLine("Start from HangFire!"));      
    }

  }
}
