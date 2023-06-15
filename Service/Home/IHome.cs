using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Home
{
    public interface IHome
    {
        List<Discussion> DiscussionList();
    }
}
