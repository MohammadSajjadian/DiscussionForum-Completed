using Data.Entities;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Forums
{
    public interface IForum
    {
        void AddForumToDataBase(ForumViewModel forumViewModel);
        void DeleteForumFromDataBase(int forumId);
        void DeleteSelectedForumsFromDataBase(List<int> forumIds);
        Forum FindForum(int forumId);
        void ConfirmUpdateForumInDataBase(ForumViewModel forumViewModel);
    }
}
