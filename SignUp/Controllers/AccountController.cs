using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SignUp.Models;
using SignUp.Providers;
using SignUp.Results;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Mail;
using System.IO;

namespace SignUp.Controllers
{
  // [Authorize]
  [AllowAnonymous]
  [RoutePrefix("api/Account")]
  public class AccountController : ApiController
  {
    private const string LocalLoginProvider = "Local";
    private ApplicationUserManager _userManager;

    public AccountController()
    {
    }

    public AccountController(ApplicationUserManager userManager,
        ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
    }

    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    [Route("UserInfo")]
    public UserInfoViewModel GetUserInfo()
    {
      ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

      return new UserInfoViewModel
      {
        Email = User.Identity.GetUserName(),
        HasRegistered = externalLogin == null,
        LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
      };
    }

    // POST api/Account/Logout
    [Route("Logout")]
    public IHttpActionResult Logout()
    {
      Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
      return Ok();
    }

    // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
    [Route("ManageInfo")]
    public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
    {
      IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

      if (user == null)
      {
        return null;
      }

      List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

      foreach (IdentityUserLogin linkedAccount in user.Logins)
      {
        logins.Add(new UserLoginInfoViewModel
        {
          LoginProvider = linkedAccount.LoginProvider,
          ProviderKey = linkedAccount.ProviderKey
        });
      }

      if (user.PasswordHash != null)
      {
        logins.Add(new UserLoginInfoViewModel
        {
          LoginProvider = LocalLoginProvider,
          ProviderKey = user.UserName,
        });
      }

      return new ManageInfoViewModel
      {
        LocalLoginProvider = LocalLoginProvider,
        Email = user.UserName,
        Logins = logins,
        ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
      };
    }


    #region Set Change Password

    // POST api/Account/ChangePassword
    [Route("ChangePassword")]
    public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await UserManager.ChangePasswordAsync(model.UserId, model.OldPassword,
          model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    // POST api/Account/SetPassword
    [Route("SetPassword")]
    public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }


    #endregion


    // POST api/Account/RemoveLogin
    [Route("RemoveLogin")]
    public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result;

      if (model.LoginProvider == LocalLoginProvider)
      {
        result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
      }
      else
      {
        result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
            new UserLoginInfo(model.LoginProvider, model.ProviderKey));
      }

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }


    #region External Functions

    // POST api/Account/AddExternalLogin
    [Route("AddExternalLogin")]
    public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

      AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

      if (ticket == null || ticket.Identity == null || (ticket.Properties != null
          && ticket.Properties.ExpiresUtc.HasValue
          && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
      {
        return BadRequest("External login failure.");
      }

      ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

      if (externalData == null)
      {
        return BadRequest("The external login is already associated with an account.");
      }

      IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
          new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }


    // GET api/Account/ExternalLogin
    [OverrideAuthentication]
    [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
    [AllowAnonymous]
    [Route("ExternalLogin", Name = "ExternalLogin")]
    public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
    {
      if (error != null)
      {
        return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
      }

      if (!User.Identity.IsAuthenticated)
      {
        return new ChallengeResult(provider, this);
      }

      ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

      if (externalLogin == null)
      {
        return InternalServerError();
      }

      if (externalLogin.LoginProvider != provider)
      {
        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        return new ChallengeResult(provider, this);
      }

      ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
          externalLogin.ProviderKey));

      bool hasRegistered = user != null;

      if (hasRegistered)
      {
        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
           OAuthDefaults.AuthenticationType);
        ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
            CookieAuthenticationDefaults.AuthenticationType);

        AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
      }
      else
      {
        IEnumerable<Claim> claims = externalLogin.GetClaims();
        ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        Authentication.SignIn(identity);
      }

      return Ok();
    }



    // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
    [AllowAnonymous]
    [Route("ExternalLogins")]
    public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
    {
      IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
      List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

      string state;

      if (generateState)
      {
        const int strengthInBits = 256;
        state = RandomOAuthStateGenerator.Generate(strengthInBits);
      }
      else
      {
        state = null;
      }

      foreach (AuthenticationDescription description in descriptions)
      {
        ExternalLoginViewModel login = new ExternalLoginViewModel
        {
          Name = description.Caption,
          Url = Url.Route("ExternalLogin", new
          {
            provider = description.AuthenticationType,
            response_type = "token",
            client_id = Startup.PublicClientId,
            redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
            state = state
          }),
          State = state
        };
        logins.Add(login);
      }

      return logins;
    }



    // POST api/Account/RegisterExternal
    [OverrideAuthentication]
    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    [Route("RegisterExternal")]
    public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var info = await Authentication.GetExternalLoginInfoAsync();
      if (info == null)
      {
        return InternalServerError();
      }

      var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

      IdentityResult result = await UserManager.CreateAsync(user);
      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      result = await UserManager.AddLoginAsync(user.Id, info.Login);
      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }
      return Ok();
    }


    #endregion


    #region Register and Send Email

    // POST api/Account/Register
    [AllowAnonymous]
    [Route("Register")]

    public async Task<IHttpActionResult> Register(AccountModel model)
    {
      var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
      var manager = new UserManager<ApplicationUser>(userStore);

      var email = await UserManager.FindByEmailAsync(model.Email);
      if (email != null)
      {

        return BadRequest("The email " + model.Email + " is already associated with an account.");
      }

      var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
//      user.EmailConfirmed = true;
      manager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 4
      };

      confirmEmail(model.firstName, model.Email, user.Id);

      IdentityResult result = manager.Create(user, model.Password);
      return Ok(result.Succeeded ? result : result); 
    }
    #endregion


    #region Confirm Email 

    public static string confirmEmail(string client, string EmailTo, string Id)
    {
      string mailBody;
      string messageResult;
      //string EmailFrom = "admin@anbcounselling.co.nz";
      string EmailFrom = "ernst@ezy.kiwi";

      //string FilePath = "E:/_Git/AB/API 2/SignUp/Views/SignUp.html";
      var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/SignUp.html");

      string FilePath = mappedPath; // "https://anbcounselling.co.nz/SignUp.html";

      StreamReader str = new StreamReader(FilePath);
      string MailText = str.ReadToEnd();
      str.Close();

      mailBody = MailText;
      mailBody = mailBody.Replace("[userId]", Id);
      mailBody = mailBody.Replace("[emailAddress]", EmailTo.Trim());
      mailBody = mailBody.Replace("[newusername]", client.Trim());      
      mailBody = mailBody.Replace("[blank]", "");


      MailMessage mm = new MailMessage(EmailFrom, EmailTo);
      MailAddress bcc = new MailAddress("ernst@hotmail.co.nz; anneke.bornman@yahoo.co.nz");
      mm.Bcc.Add(bcc);

      mm.Subject = "AnB Counselling Confirmation: " + client;
      mm.Body = mailBody;
      mm.IsBodyHtml = true;
      try
      {
        SmtpClient smtp = new SmtpClient();
        smtp.Send(mm);
      }
      catch (Exception e)
      {
        Console.Write(e.Message);
        messageResult = e.Message;
      }
      return "Email Sent";
    }

    // GET: /Account/ConfirmEmail
    [Route("ResendConfirmEmail")]
    [AllowAnonymous]
    [HttpPost]
    public IHttpActionResult ResendConfirmEmail(ResendEmailModel model)
    {
      if (model.Client == null || model.Email == null || model.Id == null)
      {
        return NotFound();
      }
      confirmEmail(model.Client, model.Email, model.Id);
      return Ok("An email has been sent to your inbox.");
    }

    //
    // GET: /Account/ConfirmEmail
    [Route("ConfirmEmail")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
      {
        return NotFound();
      }

      code = HttpUtility.UrlDecode(code);

      var result = await UserManager.ConfirmEmailAsync(userId, code);
      return Ok(result.Succeeded ? "ConfirmEmail" : "Error");
    }

    #endregion


    #region Forgot Password
    // POST: /Account/ForgotPassword
    [HttpPost]
    [Route("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await UserManager.FindByEmailAsync(model.Email);
        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        {
          // Don't reveal that the user does not exist or is not confirmed
          return BadRequest("Either user does not exist or you have not confirmed your email.");
        }

        try
        {
          // Send an email with this link
          string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
         //     string callbackUrl = "http://localhost:4200/reset-password/userId=" + user.Id + "code=" + code;
             string callbackUrl = "https://anbcounselling.co.nz/reset-password/userId=" + user.Id + "code=" + code;

          
         await UserManager.SendEmailAsync(user.Id, "Reset Password",
          "<br><br><h3>Message From AnB Counselling.</h3><br><br><h4> Please reset your password by clicking <a href=\"" + callbackUrl + "\">here.</a></h4><br><br><h2>Kind Regards<br>The AnB Counselling Team</h2>");
          return Ok("An email has been sent to your inbox.");
        }
        catch (Exception ex)
        {
          return InternalServerError();
        }

      }

      return BadRequest();
    }

    #endregion


    #region Reset Password

    // POST: /Account/ResetPassword
    [HttpPost]
    [AllowAnonymous]
    [Route("ResetPassword")]
    public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var user = await UserManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToRoute("Default", new { controller = "User" });
      }
      var result = await UserManager.ResetPasswordAsync(model.Id, model.Code, model.Password);
      if (result.Succeeded)
      {
        return Ok(user);
      }
      return InternalServerError();
    }

    #endregion



    protected override void Dispose(bool disposing)
    {
      if (disposing && _userManager != null)
      {
        _userManager.Dispose();
        _userManager = null;
      }

      base.Dispose(disposing);
    }

    // POST: /Account/ForgotPassword
    [HttpPost]
    [Route("MyTest2")]
    [AllowAnonymous]
    public async Task<IHttpActionResult> MyTest2(ForgotPasswordViewModel model)
    {
      try
      {
        var xx = model;

      }
      catch (Exception ex)
      {

      }
      return Ok();

    }


      // POST: /Account/ForgotPassword
      [HttpPost]
    [Route("MyTest")]
    [AllowAnonymous]
    public async Task<IHttpActionResult> MyTest(ForgotPasswordViewModel model)
    {
      dynamic MailMessage = new MailMessage();
      try
      {

        var EmailFrom = "admin@anbcounselling.co.nz";
      //  var EmailFrom = "ernst @ezy.kiwi";
        
         //var EmailFrom = "admin@ezy.kiwi";
         var EmailTo = "ernst@hotmail.co.nz";
        MailMessage gmail = new MailMessage(EmailFrom, EmailTo);
        gmail.Subject = "Website Testing";
        gmail.Body = "Hi daar net 'n toets";

        gmail.Subject = gmail.Subject;
        gmail.Body = gmail.Body;
        gmail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        //                SmtpClient.Send(gmail);
        smtp.Send(gmail);

      }
      catch (Exception ex)
      {
        myErrors(ex.Message);
        return BadRequest(ex.Message);
      }
      return Ok("Email Sent");

    }

    #region Helpers

    private IAuthenticationManager Authentication
    {
      get { return Request.GetOwinContext().Authentication; }
    }

    private IHttpActionResult GetErrorResult(IdentityResult result)
    {
      if (result == null)
      {
        return InternalServerError();
      }

      if (!result.Succeeded)
      {
        if (result.Errors != null)
        {
          foreach (string error in result.Errors)
          {
            ModelState.AddModelError("", error);
          }
        }

        if (ModelState.IsValid)
        {
          // No ModelState errors are available to send, so just return an empty BadRequest.
          return BadRequest();
        }

        return BadRequest(ModelState);
      }

      return null;
    }

    private class ExternalLoginData
    {
      public string LoginProvider { get; set; }
      public string ProviderKey { get; set; }
      public string UserName { get; set; }

      public IList<Claim> GetClaims()
      {
        IList<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

        if (UserName != null)
        {
          claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
        }

        return claims;
      }

      public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
      {
        if (identity == null)
        {
          return null;
        }

        Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
            || String.IsNullOrEmpty(providerKeyClaim.Value))
        {
          return null;
        }

        if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
        {
          return null;
        }

        return new ExternalLoginData
        {
          LoginProvider = providerKeyClaim.Issuer,
          ProviderKey = providerKeyClaim.Value,
          UserName = identity.FindFirstValue(ClaimTypes.Name)
        };
      }
    }

    private static class RandomOAuthStateGenerator
    {
      private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

      public static string Generate(int strengthInBits)
      {
        const int bitsPerByte = 8;

        if (strengthInBits % bitsPerByte != 0)
        {
          throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
        }

        int strengthInBytes = strengthInBits / bitsPerByte;

        byte[] data = new byte[strengthInBytes];
        _random.GetBytes(data);
        return HttpServerUtility.UrlTokenEncode(data);
      }
    }

    #endregion
    public static string myErrors(object Error)
    {
      Console.WriteLine(Error);
      return "Error";
    }



  }

}
