using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ApplicationUserViewModel
    {
        [MaxLength(30, ErrorMessage = "Max nickname length is 30")]
        public string nickName { get; set; }

        [MaxLength(100, ErrorMessage = "Nickname should be between 1 to 20 characters")]
        public string aboutMe { get; set; }
        
        public IFormFile profileImg { get; set; }

        public ApplicationUser user { get; set; }
    }
}
