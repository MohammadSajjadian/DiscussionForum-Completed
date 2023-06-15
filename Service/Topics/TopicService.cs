using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Topics
{
    public class TopicService : ITopic
    {
        private readonly GlobalFacade globalFacade;
        private readonly UserManager<ApplicationUser> userManager;
        public TopicService(GlobalFacade globalFacade, UserManager<ApplicationUser> userManager)
        {
            this.globalFacade = globalFacade;
            this.userManager = userManager;
        }

        public void AddTopicToDataBase(TopicViewModel topicViewModel, string userId)
        {
            Topic topic = new()
            {
                name = topicViewModel.name,
                description = topicViewModel.description,
                createTime = DateTime.Now,
                forumId = topicViewModel.forumId,
                userId = userId,
            };
            globalFacade.db.Add(topic);
            globalFacade.db.SaveChanges();
        }

        public void ConfirmUpdateTopicInDataBase(TopicViewModel topicViewModel)
        {
            Topic topic = FindTopic(topicViewModel.topic.id);
            topic.name = topicViewModel.name;
            topic.description = topicViewModel.description;
            topic.forumId = topicViewModel.forumId;

            globalFacade.db.Update(topic);
            globalFacade.db.SaveChanges();
        }

        public void DeleteTopicFromDataBase(int topicId)
        {
            Topic topic = FindTopic(topicId);
            globalFacade.db.Remove(topic);
            globalFacade.db.SaveChanges();
        }

        public void DeleteSelectedTopicsFromDataBase(List<int> topicIds)
        {
            foreach (var item in topicIds)
            {
                Topic topic = FindTopic(item);
                globalFacade.db.Remove(topic);
            }
            globalFacade.db.SaveChanges();
        }

        public Topic FindTopic(int topicId)
        {
            return globalFacade.db.topics.Find(topicId);
        }

        public List<Discussion> DiscussionsList()
        {
            return globalFacade.db.discussions.ToList();
        }
    }
}
