using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Email;
using Service.Facades;
using Service.Messages;

namespace Discussion_Forum.Controllers.Account
{
    public class RegisterController : Controller
    {
        #region DependencyInjection
        private readonly GlobalFacade globalFacade;
        private readonly IEmail emailServices;
        public RegisterController(GlobalFacade globalFacade, IEmail emailServices)
        {
            this.globalFacade = globalFacade;
            this.emailServices = emailServices;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(registerViewModel.userName);
            if (user is null)
            {
                user = new ApplicationUser() { UserName = registerViewModel.userName, Email = registerViewModel.userName };
                var status = await globalFacade.userManager.CreateAsync(user, registerViewModel.password);

                if (status.Succeeded)
                {
                    await CreateTokenUrl(user);
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    globalFacade.messageService.RegisterError(TempData);
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                globalFacade.messageService.UserExist(TempData);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task CreateTokenUrl(ApplicationUser user)
        {
            string token = await globalFacade.userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action(nameof(ConfirmEmail), nameof(RegisterController).Replace("Controller", ""), new { token, userId = user.Id }, "https");

            await UpdateTokenCreationTime(user);
            SendEmail(url, user.Email);
        }

        public async Task UpdateTokenCreationTime(ApplicationUser user)
        {
            user.tokenCreationTime = DateTime.Now;
            await globalFacade.userManager.UpdateAsync(user);
        }

        public void SendEmail(string url, string email)
        {
            emailServices.EmailSender("Username confirmation", $"Click on this <a href='{url}'>link</a> to verify your username.", email);
            globalFacade.messageService.EmailConfirmation(TempData);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            ApplicationUser user = await globalFacade.userManager.FindByIdAsync(userId);
            if (TokenLifeTime(user) <= TimeSpan.FromHours(1))
            {
                var status = await globalFacade.userManager.ConfirmEmailAsync(user, token);

                if (status.Succeeded)
                {
                    globalFacade.messageService.EmailConfirmed(TempData);
                    await AddUserToUserRule(user);

                    return RedirectToAction(nameof(LoginController.Index), nameof(LoginController).Replace("Controller", ""));
                }
                else
                {
                    await globalFacade.userManager.DeleteAsync(user);
                    globalFacade.messageService.EmailConfirmationFailed(TempData);

                    return RedirectToAction(nameof(RegisterController.Index), nameof(RegisterController).Replace("Controller", ""));
                }
            }
            else
            {
                await globalFacade.userManager.DeleteAsync(user);
                globalFacade.messageService.EmailConfirmationExpire(TempData);

                return RedirectToAction(nameof(RegisterController.Index), nameof(RegisterController).Replace("Controller", ""));
            }
        }

        public TimeSpan TokenLifeTime(ApplicationUser user) => DateTime.Now.Subtract(user.tokenCreationTime);

        public async Task AddUserToUserRule(ApplicationUser user) => await globalFacade.userManager.AddToRoleAsync(user, "user");

        public async Task<IActionResult> IsUserNameExist(string userName)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(userName);
            if (user is not null) return Json(false);
            else return Json(true);
        }
    }
}
