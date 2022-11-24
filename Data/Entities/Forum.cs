using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Forum
    {
        public int id { get; set; }
        public string name { get; set; }

        #region ForeignKey
        public int discussionId { get; set; }
        [ForeignKey(nameof(discussionId))]
        public Discussion discussion { get; set; }
        #endregion

        public ICollection<Topic> topics { get; set; }
    }
}
