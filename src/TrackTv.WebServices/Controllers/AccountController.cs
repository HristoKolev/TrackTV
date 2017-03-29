namespace TrackTv.WebServices.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Logging;

    using TrackTv.Data.Models;
    using TrackTv.WebServices.Models;
    using TrackTv.WebServices.Models.AccountViewModels;
    using TrackTv.WebServices.Services;

    [Authorize]
    public class AccountController : Controller
    {
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.EmailSender = emailSender;
            this.SmsSender = smsSender;
            this.Logger = loggerFactory.CreateLogger<AccountController>();
        }

        private IEmailSender EmailSender { get; }

        private ILogger Logger { get; }

        private SignInManager<User> SignInManager { get; }

        private ISmsSender SmsSender { get; }

        private UserManager<User> UserManager { get; }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return this.View("Error");
            }

            var user = await this.UserManager.FindByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var result = await this.UserManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);

            return this.View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Action("ExternalLoginCallback", "Account", new
            {
                ReturnUrl = returnUrl
            });
            var properties = this.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return this.Challenge(properties, provider);
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return this.View(nameof(Login));
            }

            var info = await this.SignInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);

            if (info == null)
            {
                return this.RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result =
                await this.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false)
                          .ConfigureAwait(false);

            if (result.Succeeded)
            {
                this.Logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);

                return this.RedirectToLocal(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return this.RedirectToAction(nameof(SendCode), new
                {
                    ReturnUrl = returnUrl
                });
            }

            if (result.IsLockedOut)
            {
                return this.View("Lockout");
            }

            // If the user does not have an account, then ask the user to create an account.
            this.ViewData["ReturnUrl"] = returnUrl;

            this.ViewData["LoginProvider"] = info.LoginProvider;

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            return this.View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
            {
                Email = email
            });
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this.SignInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);

                if (info == null)
                {
                    return this.View("ExternalLoginFailure");
                }

                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await this.UserManager.CreateAsync(user).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    result = await this.UserManager.AddLoginAsync(user, info).ConfigureAwait(false);

                    if (result.Succeeded)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                        this.Logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            this.ViewData["ReturnUrl"] = returnUrl;

            return this.View(model);
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.UserManager.FindByNameAsync(model.Email).ConfigureAwait(false);

                if (user == null || !await this.UserManager.IsEmailConfirmedAsync(user).ConfigureAwait(false))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                // var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                // await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                // $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                // return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

            return this.View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await this.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false)
                              .ConfigureAwait(false);

                if (result.Succeeded)
                {
                    this.Logger.LogInformation(1, "User logged in.");

                    return this.RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToAction(nameof(SendCode), new
                    {
                        ReturnUrl = returnUrl,
                        model.RememberMe
                    });
                }

                if (result.IsLockedOut)
                {
                    this.Logger.LogWarning(2, "User account locked out.");

                    return this.View("Lockout");
                }

                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return this.View(model);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await this.SignInManager.SignOutAsync().ConfigureAwait(false);

            this.Logger.LogInformation(4, "User logged out.");

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;

            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await this.UserManager.CreateAsync(user, model.Password).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    // await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    // $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    this.Logger.LogInformation(3, "User created a new account with password.");

                    return this.RedirectToLocal(returnUrl);
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? this.View("Error") : this.View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.UserManager.FindByNameAsync(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction(nameof(this.ResetPasswordConfirmation), "Account");
            }

            var result = await this.UserManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(this.ResetPasswordConfirmation), "Account");
            }

            this.AddErrors(result);
            return this.View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        // GET: /Account/SendCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await this.SignInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var userFactors = await this.UserManager.GetValidTwoFactorProvidersAsync(user).ConfigureAwait(false);

            var factorOptions = userFactors.Select(purpose => new SelectListItem
            {
                Text = purpose,
                Value = purpose
            }).ToList();

            return this.View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.SignInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            // Generate the token and send it
            var code = await this.UserManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(code))
            {
                return this.View("Error");
            }

            var message = "Your security code is: " + code;

            if (model.SelectedProvider == "Email")
            {
                string email = await this.UserManager.GetEmailAsync(user).ConfigureAwait(false);

                await this.EmailSender.SendEmailAsync(email, "Security Code", message).ConfigureAwait(false);
            }
            else if (model.SelectedProvider == "Phone")
            {
                string number = await this.UserManager.GetPhoneNumberAsync(user).ConfigureAwait(false);

                await this.SmsSender.SendSmsAsync(number, message).ConfigureAwait(false);
            }

            return this.RedirectToAction(nameof(VerifyCode), new
            {
                Provider = model.SelectedProvider,
                model.ReturnUrl,
                model.RememberMe
            });
        }

        // GET: /Account/VerifyCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await this.SignInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            return this.View(new VerifyCodeViewModel
            {
                Provider = provider,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result =
                await this.SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser)
                          .ConfigureAwait(false);

            if (result.Succeeded)
            {
                return this.RedirectToLocal(model.ReturnUrl);
            }

            if (result.IsLockedOut)
            {
                this.Logger.LogWarning(7, "User account locked out.");

                return this.View("Lockout");
            }

            this.ModelState.AddModelError(string.Empty, "Invalid code.");

            return this.View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<User> GetCurrentUserAsync()
        {
            return this.UserManager.GetUserAsync(this.HttpContext.User);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}