namespace TrackTv.WebServices.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using TrackTv.Models;
    using TrackTv.WebServices.Models;
    using TrackTv.WebServices.Models.ManageViewModels;
    using TrackTv.WebServices.Services;

    [Authorize]
    public class ManageController : Controller
    {
        public ManageController(
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
            this.Logger = loggerFactory.CreateLogger<ManageController>();
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,

            AddLoginSuccess,

            ChangePasswordSuccess,

            SetTwoFactorSuccess,

            SetPasswordSuccess,

            RemoveLoginSuccess,

            RemovePhoneSuccess,

            Error
        }

        private IEmailSender EmailSender { get; }

        private ILogger Logger { get; }

        private SignInManager<User> SignInManager { get; }

        private ISmsSender SmsSender { get; }

        private UserManager<User> UserManager { get; }

        // GET: /Manage/AddPhoneNumber
        public IActionResult AddPhoneNumber()
        {
            return this.View();
        }

        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // Generate the token and send it
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber).ConfigureAwait(false);

            await this.SmsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code).ConfigureAwait(false);

            return this.RedirectToAction(nameof(VerifyPhoneNumber), new
            {
                model.PhoneNumber
            });
        }

        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                var result = await this.UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    this.Logger.LogInformation(3, "User changed their password successfully.");

                    return this.RedirectToAction(nameof(this.Index), new
                    {
                        Message = ManageMessageId.ChangePasswordSuccess
                    });
                }

                this.AddErrors(result);

                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index), new
            {
                Message = ManageMessageId.Error
            });
        }

        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                await this.UserManager.SetTwoFactorEnabledAsync(user, false).ConfigureAwait(false);

                await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                this.Logger.LogInformation(2, "User disabled two-factor authentication.");
            }

            return this.RedirectToAction(nameof(this.Index), "Manage");
        }

        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                await this.UserManager.SetTwoFactorEnabledAsync(user, true).ConfigureAwait(false);

                await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                this.Logger.LogInformation(1, "User enabled two-factor authentication.");
            }

            return this.RedirectToAction(nameof(this.Index), "Manage");
        }

        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            string statusMessage;

            switch (message)
            {
                case ManageMessageId.ChangePasswordSuccess :
                    statusMessage = "Your password has been changed.";
                    break;
                case ManageMessageId.SetPasswordSuccess :
                    statusMessage = "Your password has been set.";
                    break;
                case ManageMessageId.SetTwoFactorSuccess :
                    statusMessage = "Your two-factor authentication provider has been set.";
                    break;
                case ManageMessageId.Error :
                    statusMessage = "An error has occurred.";
                    break;
                case ManageMessageId.AddPhoneSuccess :
                    statusMessage = "Your phone number was added.";
                    break;
                case ManageMessageId.RemovePhoneSuccess :
                    statusMessage = "Your phone number was removed.";
                    break;
                default :
                    statusMessage = string.Empty;
                    break;
            }

            this.ViewData["StatusMessage"] = statusMessage;

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var model = new IndexViewModel
            {
                HasPassword = await this.UserManager.HasPasswordAsync(user).ConfigureAwait(false),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(user).ConfigureAwait(false),
                TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(user).ConfigureAwait(false),
                Logins = await this.UserManager.GetLoginsAsync(user).ConfigureAwait(false),
                BrowserRemembered = await this.SignInManager.IsTwoFactorClientRememberedAsync(user).ConfigureAwait(false)
            };

            return this.View(model);
        }

        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = this.Url.Action("LinkLoginCallback", "Manage");

            var properties = this.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl,
                this.UserManager.GetUserId(this.User));

            return this.Challenge(properties, provider);
        }

        // GET: /Manage/LinkLoginCallback
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            string expectedXsrf = await this.UserManager.GetUserIdAsync(user).ConfigureAwait(false);

            var info = await this.SignInManager.GetExternalLoginInfoAsync(expectedXsrf).ConfigureAwait(false);

            if (info == null)
            {
                return this.RedirectToAction(nameof(this.ManageLogins), new
                {
                    Message = ManageMessageId.Error
                });
            }

            var result = await this.UserManager.AddLoginAsync(user, info).ConfigureAwait(false);

            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;

            return this.RedirectToAction(nameof(this.ManageLogins), new
            {
                Message = message
            });
        }

        // GET: /Manage/ManageLogins
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            string statusMessage;

            switch (message)
            {
                case ManageMessageId.RemoveLoginSuccess :
                    statusMessage = "The external login was removed.";
                    break;
                case ManageMessageId.AddLoginSuccess :
                    statusMessage = "The external login was added.";
                    break;
                case ManageMessageId.Error :
                    statusMessage = "An error has occurred.";
                    break;
                default :
                    statusMessage = string.Empty;
                    break;
            }

            this.ViewData["StatusMessage"] = statusMessage;

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var userLogins = await this.UserManager.GetLoginsAsync(user).ConfigureAwait(false);

            var otherLogins =
                this.SignInManager.GetExternalAuthenticationSchemes()
                    .Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider))
                    .ToList();

            this.ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;

            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                var result = await this.UserManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }

            return this.RedirectToAction(nameof(this.ManageLogins), new
            {
                Message = message
            });
        }

        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                var result = await this.UserManager.SetPhoneNumberAsync(user, null).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    return this.RedirectToAction(nameof(this.Index), new
                    {
                        Message = ManageMessageId.RemovePhoneSuccess
                    });
                }
            }

            return this.RedirectToAction(nameof(this.Index), new
            {
                Message = ManageMessageId.Error
            });
        }

        // GET: /Manage/SetPassword
        [HttpGet]
        public IActionResult SetPassword()
        {
            return this.View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                var result = await this.UserManager.AddPasswordAsync(user, model.NewPassword).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    return this.RedirectToAction(nameof(this.Index), new
                    {
                        Message = ManageMessageId.SetPasswordSuccess
                    });
                }

                this.AddErrors(result);

                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index), new
            {
                Message = ManageMessageId.Error
            });
        }

        // GET: /Manage/VerifyPhoneNumber
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user == null)
            {
                return this.View("Error");
            }

            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber).ConfigureAwait(false);

            // Send an SMS to verify the phone number
            if (phoneNumber == null)
            {
                return this.View("Error");
            }

            return this.View(new VerifyPhoneNumberViewModel
            {
                PhoneNumber = phoneNumber
            });
        }

        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync().ConfigureAwait(false);

            if (user != null)
            {
                var result = await this.UserManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                    return this.RedirectToAction(nameof(this.Index), new
                    {
                        Message = ManageMessageId.AddPhoneSuccess
                    });
                }
            }

            // If we got this far, something failed, redisplay the form
            this.ModelState.AddModelError(string.Empty, "Failed to verify phone number");

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
    }
}