using Data.Entities;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Discussions
{
    public class DiscussionService : IDiscussion
    {
        private readonly GlobalFacade globalFacade;
        public DiscussionService(GlobalFacade globalFacade)
        {
            this.globalFacade = globalFacade;
        }

        public void AddDiscussionToDataBase(DiscussionViewModel discussionViewModel)
        {
            Discussion discussion = new() { name = discussionViewModel.name };
            globalFacade.db.Add(discussion);
            globalFacade.db.SaveChanges();
        }

        public void DeleteDiscussionFromDataBase(int discussionId)
        {
            Discussion discussion = FindDiscussion(discussionId);
            globalFacade.db.Remove(discussion);
            globalFacade.db.SaveChanges();
        }

        public void DeleteSelectedDiscussionsFromDataBase(List<int> discussionIds)
        {
            foreach (var item in discussionIds)
            {
                Discussion discussion = FindDiscussion(item);
                globalFacade.db.Remove(discussion);
            }
            globalFacade.db.SaveChanges();
        }

        public void ConfirmUpdateDiscussionInDataBase(DiscussionViewModel discussionViewModel)
        {
            Discussion discussion = FindDiscussion(discussionViewModel.id);
            discussion.name = discussionViewModel.name;

            globalFacade.db.Update(discussion);
            globalFacade.db.SaveChanges();
        }

        public Discussion FindDiscussion(int discussionId)
        {
            return globalFacade.db.discussions.Find(discussionId);
        }
    }
}
