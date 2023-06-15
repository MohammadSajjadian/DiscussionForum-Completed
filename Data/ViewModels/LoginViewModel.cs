using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Enter UserName")]
        public string userName { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        public string password { get; set; }
        
        public bool rememberMe { get; set; }
    }
}
