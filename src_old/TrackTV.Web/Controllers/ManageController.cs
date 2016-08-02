namespace TrackTV.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using TrackTV.Models;
    using TrackTV.Web.Identity;
    using TrackTV.Web.ViewModels.Manage;

    [Authorize]
    public class ManageController : Controller
    {
        private const string XsrfKey = "XsrfId";

        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult AddPhoneNumber()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // Generate the token and send it
            string code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(this.User.Identity.GetUserId(), model.Number);

            if (this.UserManager.SmsService != null)
            {
                IdentityMessage message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };

                await this.UserManager.SmsService.SendAsync(message);
            }

            return this.RedirectToAction(
                "VerifyPhoneNumber",
                new
                {
                    PhoneNumber = model.Number
                });
        }

        public ActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            IdentityResult result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, false, false);
                }

                return this.RedirectToAction(
                    "Index",
                    new
                    {
                        Message = ManageMessageId.ChangePasswordSuccess
                    });
            }

            this.AddErrors(result);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.User.Identity.GetUserId(), false);

            ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, false, false);
            }

            return this.RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.User.Identity.GetUserId(), true);

            ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, false, false);
            }

            return this.RedirectToAction("Index", "Manage");
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            switch (message)
            {
                case ManageMessageId.AddPhoneSuccess:
                    this.ViewBag.StatusMessage = "Your phone number was added.";
                    break;
                case ManageMessageId.ChangePasswordSuccess:
                    this.ViewBag.StatusMessage = "Your password has been changed.";
                    break;
                case ManageMessageId.SetTwoFactorSuccess:
                    this.ViewBag.StatusMessage = "Your two-factor authentication provider has been set.";
                    break;
                case ManageMessageId.SetPasswordSuccess:
                    this.ViewBag.StatusMessage = "Your password has been set.";
                    break;

                case ManageMessageId.RemovePhoneSuccess:
                    this.ViewBag.StatusMessage = "Your phone number was removed.";
                    break;
                case ManageMessageId.Error:
                    this.ViewBag.StatusMessage = "An error has occurred.";
                    break;
                case ManageMessageId.RemoveLoginSuccess:
                case null:
                    this.ViewBag.StatusMessage = string.Empty;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("message");
            }

            string userId = this.User.Identity.GetUserId();

            IndexViewModel model = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await this.UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, this.Url.Action("LinkLoginCallback", "Manage"), this.User.Identity.GetUserId());
        }

        public async Task<ActionResult> LinkLoginCallback()
        {
            ExternalLoginInfo loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, this.User.Identity.GetUserId());

            if (loginInfo == null)
            {
                return this.RedirectToAction(
                    "ManageLogins",
                    new
                    {
                        Message = ManageMessageId.Error
                    });
            }

            IdentityResult result = await this.UserManager.AddLoginAsync(this.User.Identity.GetUserId(), loginInfo.Login);

            if (result.Succeeded)
            {
                return this.RedirectToAction("ManageLogins");
            }

            return this.RedirectToAction(
                "ManageLogins",
                new
                {
                    Message = ManageMessageId.Error
                });
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            if (message == ManageMessageId.RemoveLoginSuccess)
            {
                this.ViewBag.StatusMessage = "The external login was removed.";
            }
            else if (message == ManageMessageId.Error)
            {
                this.ViewBag.StatusMessage = "An error has occurred.";
            }
            else
            {
                this.ViewBag.StatusMessage = string.Empty;
            }

            ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

            if (user == null)
            {
                return this.View("Error");
            }

            IList<UserLoginInfo> userLogins = await this.UserManager.GetLoginsAsync(this.User.Identity.GetUserId());

            List<AuthenticationDescription> otherLogins =
                this.AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();

            this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            return this.View(
                new ManageLoginsViewModel
                {
                    CurrentLogins = userLogins,
                    OtherLogins = otherLogins
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;

            IdentityResult result = await this.UserManager.RemoveLoginAsync(this.User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));

            if (result.Succeeded)
            {
                ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, false, false);
                }

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return this.RedirectToAction(
                "ManageLogins",
                new
                {
                    Message = message
                });
        }

        public async Task<ActionResult> RemovePhoneNumber()
        {
            IdentityResult result = await this.UserManager.SetPhoneNumberAsync(this.User.Identity.GetUserId(), null);

            if (!result.Succeeded)
            {
                return this.RedirectToAction(
                    "Index",
                    new
                    {
                        Message = ManageMessageId.Error
                    });
            }

            ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, false, false);
            }

            return this.RedirectToAction(
                "Index",
                new
                {
                    Message = ManageMessageId.RemovePhoneSuccess
                });
        }

        public ActionResult SetPassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                IdentityResult result = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, false, false);
                    }

                    return this.RedirectToAction(
                        "Index",
                        new
                        {
                            Message = ManageMessageId.SetPasswordSuccess
                        });
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            string code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(this.User.Identity.GetUserId(), phoneNumber);

            // Send an SMS through the SMS provider to verify the phone number
            if (phoneNumber == null)
            {
                return this.View("Error");
            }

            return this.View(
                new VerifyPhoneNumberViewModel
                {
                    PhoneNumber = phoneNumber
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            IdentityResult result = await this.UserManager.ChangePhoneNumberAsync(this.User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, false, false);
                }

                return this.RedirectToAction(
                    "Index",
                    new
                    {
                        Message = ManageMessageId.AddPhoneSuccess
                    });
            }

            // If we got this far, something failed, redisplay form
            this.ModelState.AddModelError(string.Empty, "Failed to verify phone");

            return this.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        // Used for XSRF protection when adding external logins
        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            ApplicationUser user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        private bool HasPhoneNumber()
        {
            ApplicationUser user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }

            return false;
        }
    }
}