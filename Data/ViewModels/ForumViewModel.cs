using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ForumViewModel
    {
        [Required(ErrorMessage ="Please enter forum name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter the name of the relevant discussion")]
        public int discussionId { get; set; }

        public List<Discussion> discussions { get; set; }
        public Forum forum { get; set; }
    }
}
