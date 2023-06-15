using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
    public interface IProfile
    {
        List<Topic> TopicList(ApplicationUser user);
        List<Post> PostList(ApplicationUser user);
        bool IsProfileImgNull(IFormFile profileImg);
        bool AllowedProfileImgSize(long length);
        bool AllowedExtensionName(string name);
        void UpdateProfileImg(IFormFile profileImgIFormFile, ApplicationUser user);
        Task ConfirmUpdateProfileInDataBase(ApplicationUser user);
    }
}
