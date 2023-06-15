using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Home
{
    public class HomeService : IHome
    {
        private readonly GlobalFacade globalFacade;
        public HomeService(GlobalFacade globalFacade)
        {
            this.globalFacade = globalFacade;
        }

        public List<Discussion> DiscussionList()
            => globalFacade.db.discussions.Include(x => x.forums).ThenInclude(x => x.topics).ThenInclude(x => x.ApplicationUser).OrderBy(x => x.name).ToList();
    }
}
