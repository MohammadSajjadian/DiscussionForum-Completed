using Data.Entities;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Topics
{
    public interface ITopic
    {
        void AddTopicToDataBase(TopicViewModel topicViewModel, string userId);
        void DeleteTopicFromDataBase(int TopicId);
        void DeleteSelectedTopicsFromDataBase(List<int> topicIds);
        Topic FindTopic(int topicId);
        void ConfirmUpdateTopicInDataBase(TopicViewModel topicViewModel);
        List<Discussion> DiscussionsList();
    }
}
