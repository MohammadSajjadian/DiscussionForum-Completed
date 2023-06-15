using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Topic : CommonAttributes
    {
        public int id { get; set; }
        public string name { get; set; }

        #region ForeignKey
        public int forumId { get; set; }
        [ForeignKey(nameof(forumId))]
        public Forum Forum { get; set; }

        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public ApplicationUser ApplicationUser { get; set; }
        #endregion

        public ICollection<Post> posts { get; set; }
    }
}
