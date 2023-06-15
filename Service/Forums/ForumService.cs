using Data.Context;
using Data.Entities;
using Data.ViewModels;
using Service.Facades;
using Service.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Forums
{
    public class ForumService : IForum
    {
        private readonly GlobalFacade globalFacade;
        public ForumService(GlobalFacade globalFacade)
        {
            this.globalFacade = globalFacade;
        }

        public void AddForumToDataBase(ForumViewModel forumViewModel)
        {
            Forum forum = new() { name = forumViewModel.name, discussionId = forumViewModel.discussionId };
            globalFacade.db.Add(forum);
            globalFacade.db.SaveChanges();
        }

        public void ConfirmUpdateForumInDataBase(ForumViewModel forumViewModel)
        {
            Forum forum = FindForum(forumViewModel.forum.id);
            forum.name = forumViewModel.name;
            forum.discussionId = forumViewModel.discussionId;

            globalFacade.db.Update(forum);
            globalFacade.db.SaveChanges();
        }

        public void DeleteForumFromDataBase(int forumId)
        {
            Forum forum = FindForum(forumId);
            globalFacade.db.Remove(forum);
            globalFacade.db.SaveChanges();
        }

        public void DeleteSelectedForumsFromDataBase(List<int> forumIds)
        {
            foreach (var item in forumIds)
            {
                Forum forum = FindForum(item);
                globalFacade.db.Remove(forum);
            }
            globalFacade.db.SaveChanges();
        }

        public Forum FindForum(int forumId)
        {
            return globalFacade.db.forums.Find(forumId);
        }
    }
}
