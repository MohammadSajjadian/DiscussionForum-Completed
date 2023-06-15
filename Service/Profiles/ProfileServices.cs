using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using Service.ResizeImage;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public class ProfileServices : IProfile
    {
        #region Dependency Injection
        private readonly UserManager<ApplicationUser> userManager;
        private readonly GlobalFacade globalFacade;
        private readonly IResize resizeService;
        public ProfileServices(UserManager<ApplicationUser> userManager, GlobalFacade globalFacade, IResize resizeService)
        {
            this.userManager = userManager;
            this.globalFacade = globalFacade;
            this.resizeService = resizeService;
        }
        #endregion

        public List<Topic> TopicList(ApplicationUser user)
        {
            return globalFacade.db.topics.Include(x => x.Forum).Where(t => t.ApplicationUser.Id == user.Id).OrderByDescending(x => x.id).ToList();
        }

        public List<Post> PostList(ApplicationUser user)
        {
            return globalFacade.db.posts.Include(x => x.topic).Where(p => p.ApplicationUser.Id == user.Id).Take(50).OrderByDescending(x => x.id).ToList();
        }

        public void UpdateProfileImg(IFormFile profileImgIFormFile, ApplicationUser user)
        {
            if (IsProfileImgNull(profileImgIFormFile) && AllowedProfileImgSize(profileImgIFormFile.Length))
            {
                if (AllowedExtensionName(profileImgIFormFile.FileName))
                {
                    byte[] imgLength = new byte[profileImgIFormFile.Length];
                    profileImgIFormFile.OpenReadStream().Read(imgLength, 0, imgLength.Length);
                    user.profileImg = resizeService.ImageResizer(imgLength, 100, 100, ImageFormat.Jpeg);
                }
            }
        }

        public bool IsProfileImgNull(IFormFile profileImg)
        {
            if (profileImg != null)
                return true;

            return false;
        }

        public bool AllowedProfileImgSize(long length)
        {
            if (length < Math.Pow(2, 1024))
                return true;

            return false;
        }

        public bool AllowedExtensionName(string name)
        {
            if (Path.GetExtension(name) == ".jpg")
                return true;

            return false;
        }

        public async Task ConfirmUpdateProfileInDataBase(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
            globalFacade.db.SaveChanges();
        }
    }
}
