using Data.Entities;
using Data.ViewModels;
using Discussion_Forum.Controllers.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using Service.Topics;

namespace Discussion_Topic.Controllers.CRUD_s
{
    [Authorize]
    public class TopicController : Controller
    {
        #region Dependency Injection
        private readonly GlobalFacade globalFacade;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITopic topicService;
        public TopicController(GlobalFacade globalFacade, ITopic topicService, UserManager<ApplicationUser> userManager)
        {
            this.globalFacade = globalFacade;
            this.userManager = userManager;
            this.topicService = topicService;
        }
        #endregion

        [AllowAnonymous]
        [HttpGet("Topic/{topicName}/{topicId}")]
        public IActionResult Index(int topicId, string topicName)
        {
            globalFacade.db.topics.Include(x => x.posts).ThenInclude(x => x.likes).Include(x => x.ApplicationUser).ToList();

            return View(topicService.FindTopic(topicId));
        }

        public IActionResult AddTopic()
        {
            List<Discussion> discussions = topicService.DiscussionsList();
            return View(new TopicViewModel { discussions = discussions });
        }

        public async Task<IActionResult> ConfirmAddTopic(TopicViewModel topicViewModel)
        {
            try
            {
                ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
                topicService.AddTopicToDataBase(topicViewModel, user.Id);
                globalFacade.messageService.AddTopicSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.AddTopicError(TempData);
            }
            return RedirectToAction(nameof(AddTopic));
        }

        public IActionResult DeleteTopic(int topicId)
        {
            try
            {
                topicService.DeleteTopicFromDataBase(topicId);
                globalFacade.messageService.RemoveTopicSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveTopicError(TempData);
            }
            return RedirectToAction(nameof(ProfileController.Index), nameof(ProfileController).Replace("Controller", ""));
        }

        public IActionResult DeleteSelectedTopics(List<int> topicIds)
        {
            try
            {
                topicService.DeleteSelectedTopicsFromDataBase(topicIds);
                globalFacade.messageService.RemoveTopicSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveTopicError(TempData);
            }
            return RedirectToAction(nameof(ProfileController.Index), nameof(ProfileController).Replace("Controller", ""));
        }

        [HttpGet("/Topic/UpdateTopic/{topicId}")]
        public async Task<IActionResult> UpdateTopic(int topicId)
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Topic topic = topicService.FindTopic(topicId);
            if (user.Id != topic.userId)
            {
                return BadRequest();
            }

            globalFacade.db.topics.Include(x => x.Forum).ThenInclude(x => x.discussion).ToList();

            List<Discussion> discussions = topicService.DiscussionsList();
            List<Forum> forums = globalFacade.db.forums.Where(x => x.discussionId == topic.Forum.discussion.id && x.name != topic.Forum.name).ToList();

            return View(new TopicViewModel { topic = topic, discussions = discussions, forums = forums });
        }

        public IActionResult ConfirmUpdateTopic(TopicViewModel topicViewModel)
        {
            try
            {
                topicService.ConfirmUpdateTopicInDataBase(topicViewModel);
                globalFacade.messageService.UpdateTopicSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.UpdateTopicError(TempData);
            }
            return RedirectToAction(nameof(ProfileController.Index), nameof(ProfileController).Replace("Controller", ""));
        }

        public IActionResult SelectDiscussionForums(int discussionId) => Json(globalFacade.db.forums.Where(x => x.discussionId == discussionId).OrderBy(x => x.name).ToList());
    }
}
