using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class DiscussionViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Please enter discussion name")]
        public string name { get; set; }
    }
}
