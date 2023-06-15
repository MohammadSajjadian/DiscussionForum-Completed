using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class TopicViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Please enter topic name")]
        public string name { get; set; }

        [Required(ErrorMessage ="Please enter topic description")]
        [MaxLength(1000, ErrorMessage ="Max length is 1000 charecters")]
        public string description { get; set; }

        public DateTime createTime { get; set; }

        [Required(ErrorMessage = "Please enter the name of the relevant discussion")]
        public int discussionId { get; set; }

        [Required(ErrorMessage = "Please enter the name of the relevant forum")]
        public int forumId { get; set; }

        public List<Discussion> discussions { get; set; }
        public List<Forum> forums { get; set; }
        public Topic topic { get; set; }
    }
}
