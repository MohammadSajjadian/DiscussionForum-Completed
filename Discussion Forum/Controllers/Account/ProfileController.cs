using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using Service.Profiles;
using Service.ResizeImage;
using System.Drawing.Imaging;

namespace Discussion_Forum.Controllers.Account
{
    [Authorize]
    public class ProfileController : Controller
    {
        #region Dependency Injections
        private readonly GlobalFacade globalFacade;
        private readonly IProfile profileServices;
        public ProfileController(UserManager<ApplicationUser> userManager, GlobalFacade globalFacade, IProfile profileServices)
        {
            this.globalFacade = globalFacade;
            this.profileServices = profileServices;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            globalFacade.db.Users.Include(x => x.topics).ThenInclude(x => x.Forum).Include(x => x.posts).ToList();

            return View(await globalFacade.userManager.FindByNameAsync(User.Identity.Name));
        }

        public async Task<IActionResult> UpdateProfile()
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);
            return View(new ApplicationUserViewModel { user = user });
        }

        public async Task<IActionResult> ConfirmEditProfile(ApplicationUserViewModel model)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);
            user.nickName = model.nickName;
            user.aboutMe = model.aboutMe;

            profileServices.UpdateProfileImg(model.profileImg, user);

            await profileServices.ConfirmUpdateProfileInDataBase(user);

            globalFacade.messageService.UpdateProfileSucceeded(TempData);
            return RedirectToAction(nameof(UpdateProfile));
        }

        public IActionResult ChangePassword() => View();

        public async Task<IActionResult> ChangePasswordConfirm(string currentPassword, string newPassword)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);

            var status = await globalFacade.userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (status.Succeeded)
            {
                globalFacade.messageService.PasswordChangeSucceeded(TempData);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                globalFacade.messageService.PasswordChangeError(TempData);
                return RedirectToAction(nameof(ChangePassword));
            }
        }
    }
}
