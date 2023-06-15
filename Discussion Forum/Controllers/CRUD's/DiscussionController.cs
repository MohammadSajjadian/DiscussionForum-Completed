using Data.Context;
using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Service.Discussions;
using Service.Facades;
using Service.Messages;
using System.Linq.Expressions;

namespace Discussion_Forum.Controllers.CRUD_s
{
    [Authorize(policy: "adminPolicy")]
    public class DiscussionController : Controller
    {
        #region Dependency Injection
        private readonly GlobalFacade globalFacade;
        private readonly IDiscussion discussionService;
        public DiscussionController(GlobalFacade globalFacade, IDiscussion discussionService)
        {
            this.globalFacade = globalFacade;
            this.discussionService = discussionService;
        }
        #endregion

        [AllowAnonymous]
        [HttpGet("/Discussion/{discussionName}/{discussionId}")]
        public IActionResult ShowDiscussion(int discussionId, string discussionName)
        {
            globalFacade.db.discussions.Include(x => x.forums).ThenInclude(x => x.topics).ThenInclude(x => x.ApplicationUser).ToList();

            return View(discussionService.FindDiscussion(discussionId));
        }

        public IActionResult AddDiscussion() => View();

        public IActionResult ConfirmAddDiscussion(DiscussionViewModel discussionViewModel)
        {
            try
            {
                discussionService.AddDiscussionToDataBase(discussionViewModel);
                globalFacade.messageService.AddDiscussionSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.AddDiscussionError(TempData);
            }
            return RedirectToAction(nameof(AddDiscussion));
        }

        public IActionResult DiscussionList() => View(globalFacade.db.discussions.OrderBy(x => x.name).ToList());

        public IActionResult DeleteDiscussion(int discussionId)
        {
            try
            {
                discussionService.DeleteDiscussionFromDataBase(discussionId);
                globalFacade.messageService.RemoveDiscussionSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveDiscussionError(TempData);
            }
            return RedirectToAction(nameof(DiscussionList));
        }

        public IActionResult DeleteSelectedDiscussions(List<int> discussionIds)
        {
            try
            {
                discussionService.DeleteSelectedDiscussionsFromDataBase(discussionIds);
                globalFacade.messageService.RemoveDiscussionSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveDiscussionError(TempData);
            }
            return RedirectToAction(nameof(DiscussionList));
        }

        [HttpGet("/Discussion/UpdateDiscussion/{discussionId}")]
        public IActionResult UpdateDiscussion(int discussionId)
        {
            Discussion discussion = discussionService.FindDiscussion(discussionId);
            return View(new DiscussionViewModel { id = discussion.id, name = discussion.name});
        }

        public IActionResult ConfirmUpdateDiscussion(DiscussionViewModel discussionViewModel)
        {
            try
            {
                discussionService.ConfirmUpdateDiscussionInDataBase(discussionViewModel);
                globalFacade.messageService.UpdateDiscussionSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.UpdateDiscussionError(TempData);
            }
            return RedirectToAction(nameof(DiscussionList));
        }
    }
}
