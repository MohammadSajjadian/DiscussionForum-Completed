using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Post : CommonAttributes
    {
        public int id { get; set; }

        #region ForeignKeys
        public int topicId { get; set; }
        [ForeignKey(nameof(topicId))]
        public Topic topic { get; set; }

        public string userId { get; set; } = null!;
        [ForeignKey(nameof(userId))]
        public ApplicationUser ApplicationUser { get; set; }
        #endregion

        #region ICollections
        public ICollection<Like> likes { get; set; }
        #endregion
    }
}
