using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Discussion
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Forum> forums { get; set; }
    }
}
