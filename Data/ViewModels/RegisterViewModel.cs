using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class RegisterViewModel
    {
        private const string remoteActionName = "IsUserNameExist";
        private const string remoteControllerName = "Register";

        [Required(ErrorMessage = "Please Enter UserName")]
        [Remote(remoteActionName, remoteControllerName, ErrorMessage = "UserName Already Exist")]
        public string userName { get; set; }
        
        [Required(ErrorMessage = "Please Enter Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please Enter RePassword")]
        [Compare(nameof(password), ErrorMessage = "Password and RePassword are Not Match")]
        public string rePassword { get; set; }
    }
}
