using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Email;

namespace Discussion_Forum.Controllers.Account
{
    public class RegisterController : CustomController
    {
        #region DependencyInjection
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmail emailServices;
        public RegisterController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, IEmail _emailServices)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            emailServices = _emailServices;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel)
        {
            ApplicationUser user = await userManager.FindByNameAsync(registerViewModel.userName);
            if (user == null)
            {
                user = new ApplicationUser() { UserName = registerViewModel.userName, Email = registerViewModel.userName };
                var status = await userManager.CreateAsync(user, registerViewModel.password);
                await IsRegisterSucceeded(status.Succeeded, user);

                // Just return somthing to avoid getting "not all code paths return a value" error.
                return Ok();
            }
            else
            {
                TempData[error] = userExist;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> IsRegisterSucceeded(bool succeeded, ApplicationUser user)
        {
            if (succeeded == true)
            {
                await CreateTokenUrl(user);

                // Just return somthing to avoid getting "not all code paths return a value" error.
                return Ok();
            }
            else
            {
                TempData[error] = registerErrorMessage;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task CreateTokenUrl(ApplicationUser user)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action(nameof(ConfirmEmail), nameof(RegisterControllerName), new { token, userId = user.Id }, "https");

            await UpdateTokenCreationTime(user);
            SendEmail(url, user.Email);
        }

        public async Task UpdateTokenCreationTime(ApplicationUser user)
        {
            user.tokenCreationTime = DateTime.Now;
            await userManager.UpdateAsync(user);
        }

        public IActionResult SendEmail(string url, string email)
        {
            emailServices.EmailSender(emailConfirmationSubject, $"{emailConfirmationBody}{url}", email);
            return RedirectToAction(homeIndexActionName, homeControllerName);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (DateTime.Now.Subtract(user.tokenCreationTime) <= TimeSpan.FromHours(1))
            {
                var status = await userManager.ConfirmEmailAsync(user, token);
                await IsEmailConfirmed(status.Succeeded, user);
            }
            else
            {
                await userManager.DeleteAsync(user);
                TempData[error] = emailConfirmationExpire;
            }
            return RedirectToAction(homeIndexActionName, homeControllerName);
        }

        public async Task IsEmailConfirmed(bool succeeded, ApplicationUser user)
        {
            if (succeeded == true) TempData[success] = emailConfirmed;
            else
            {
                await userManager.DeleteAsync(user);
                TempData[error] = emailConfirmationFailed;
            }
        }

        public async Task<IActionResult> IsUserNameExist(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user != null) return Json(true);
            else return Json(false);
        }
    }
}
