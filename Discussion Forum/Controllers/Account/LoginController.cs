using Data.Entities;
using Data.ViewModels;
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
                IsUserLockedOut(status.IsLockedOut);
                IsLoginSucceeded(status.Succeeded);

                // Just return somthing to avoid getting "not all code paths return a value" error.
                return Ok();
            }
            else
            {
                TempData[error] = userNotFound;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult IsUserLockedOut(bool isLockedOut)
        {
            if (isLockedOut == true)
            {
                TempData[warning] = lockedOutMessage;
                return RedirectToAction(nameof(homeIndexActionName), nameof(homeControllerName));
            }
            else return Ok();
        }

        public IActionResult IsLoginSucceeded(bool succeeded)
        {
            if (succeeded == true) return RedirectToAction(nameof(homeIndexActionName), nameof(homeControllerName));
            else
            {
                TempData[error] = loginError;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
