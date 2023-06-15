using Data.Entities;
using Data.ViewModels;
using Extension.LockedOut;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Facades;
using Service.Messages;

namespace Discussion_Forum.Controllers.Account
{
    public class LoginController : Controller
    {
        #region DependencyInjection
        private readonly GlobalFacade globalFacade;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController(GlobalFacade globalFacade, SignInManager<ApplicationUser> signInManager)
        {
            this.globalFacade = globalFacade;
            this.signInManager = signInManager;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel loginViewModel)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(loginViewModel.userName);
            if (user != null)
            {
                var status = await signInManager.PasswordSignInAsync(user, loginViewModel.password, loginViewModel.rememberMe, true);

                if (status.IsLockedOut)
                {
                    globalFacade.messageService.LockedOutTrue(TempData);
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
                }

                if (status.Succeeded) return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
                else
                {
                    globalFacade.messageService.LoginError(TempData);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                globalFacade.messageService.UserNotFound(TempData);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
