using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Save : CommonAttributes
    {
        public int id { get; set; }

        #region ForeignKeys
        public string userId { get; set; } = null!;
        [ForeignKey(nameof(userId))]
        public ApplicationUser applicationUser { get; set; }

        public int postId { get; set; }
        [ForeignKey(nameof(postId))]
        public Post Post { get; set; }
        #endregion
    }
}
