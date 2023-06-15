using Data.Entities;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Discussions
{
    public interface IDiscussion
    {
        void AddDiscussionToDataBase(DiscussionViewModel discussionViewModel);
        void DeleteDiscussionFromDataBase(int discussionId);
        void DeleteSelectedDiscussionsFromDataBase(List<int> discussionIds);
        void ConfirmUpdateDiscussionInDataBase(DiscussionViewModel discussionViewModel);
        Discussion FindDiscussion(int discussionId);
    }
}
