using Data.Entities;
using Data.ViewModels;
using Extension.LockedOut;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Discussion_Forum.Controllers.Account
{
    public class LoginController : CustomController
    {
        #region DependencyInjection
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel loginViewModel)
        {
            ApplicationUser user = await userManager.FindByNameAsync(loginViewModel.userName);
            if (user != null)
            {
                var status = await signInManager.PasswordSignInAsync(user, loginViewModel.password, loginViewModel.rememberMe, true);

                if (status.IsLockedOut == true)
                {
                    TempData[warning] = lockedOutMessage;
                    return RedirectToAction(index, homeControllerName);
                }

                if (status.Succeeded == true) return RedirectToAction(index, homeControllerName);
                else
                {
                    TempData[error] = loginError;
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData[error] = userNotFound;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(index, homeControllerName);
        }
    }
}
