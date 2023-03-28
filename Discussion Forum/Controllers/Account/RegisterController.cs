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

                if (status.Succeeded == true)
                {
                    await CreateTokenUrl(user);
                    return RedirectToAction(index, homeControllerName);
                }
                else
                {
                    TempData[error] = registerErrorMessage;
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                TempData[error] = userExist;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task CreateTokenUrl(ApplicationUser user)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action(nameof(ConfirmEmail), RegisterControllerName, new { token, userId = user.Id }, "https");

            await UpdateTokenCreationTime(user);
            SendEmail(url, user.Email);
        }

        public async Task UpdateTokenCreationTime(ApplicationUser user)
        {
            user.tokenCreationTime = DateTime.Now;
            await userManager.UpdateAsync(user);
        }

        public void SendEmail(string url, string email)
        {
            emailServices.EmailSender(emailConfirmationSubject, $"جهت تایید نام کاربری روی این <a href='{url}'>لینک</a> کلیک کنید.", email);
            TempData[success] = emailConfirmationMessage;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (DateTime.Now.Subtract(user.tokenCreationTime) <= TimeSpan.FromHours(1))
            {
                var status = await userManager.ConfirmEmailAsync(user, token);

                if (status.Succeeded)
                {
                    TempData[success] = emailConfirmed;
                    await AddUserToUserRule(user);

                    return RedirectToAction(index, LoginControllerName);
                }
                else
                {
                    await userManager.DeleteAsync(user);
                    TempData[error] = emailConfirmationFailed;

                    return RedirectToAction(index, RegisterControllerName);
                }
            }
            else
            {
                await userManager.DeleteAsync(user);
                TempData[error] = emailConfirmationExpire;

                return RedirectToAction(index, RegisterControllerName);
            }
        }

        public async Task AddUserToUserRule(ApplicationUser user)
        {
            await userManager.AddToRoleAsync(user, "user");
        }

        public async Task<IActionResult> IsUserNameExist(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user != null) return Json(false);
            else return Json(true);
        }
    }
}
