using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class PostViewModel
    {
        public int id { get; set; }
        public int topicId { get; set; }

        [Required(ErrorMessage = "Please enter post description")]
        public string description { get; set; }

        public Post post { get; set; }
        public ApplicationUser user { get; set; }
    }
}
