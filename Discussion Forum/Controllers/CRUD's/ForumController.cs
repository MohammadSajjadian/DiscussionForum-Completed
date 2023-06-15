using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Discussions;
using Service.Facades;
using Service.Forums;

namespace Discussion_Forum.Controllers.CRUD_s
{
    public class ForumController : Controller
    {
        #region Dependency Injection
        private readonly GlobalFacade globalFacade;
        private readonly IForum forumService;
        public ForumController(GlobalFacade globalFacade, IForum forumService)
        {
            this.globalFacade = globalFacade;
            this.forumService = forumService;
        }
        #endregion

        [HttpGet("/Forum/{forumName}/{forumId}")]
        public IActionResult ShowForum(int forumId, string forumName)
        {
            globalFacade.db.forums.Include(f => f.topics).ThenInclude(t => t.posts).ThenInclude(f => f.ApplicationUser).ToList();

            return View(forumService.FindForum(forumId));
        }

        public IActionResult AddForum()
        {
            List<Discussion> discussions = globalFacade.db.discussions.OrderBy(x => x.name).ToList();
            return View(new ForumViewModel { discussions = discussions });
        }

        public IActionResult ConfirmAddForum(ForumViewModel forumViewModel)
        {
            try
            {
                forumService.AddForumToDataBase(forumViewModel);
                globalFacade.messageService.AddForumSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.AddForumError(TempData);
            }
            return RedirectToAction(nameof(AddForum));
        }

        public IActionResult ForumList() => View(globalFacade.db.forums.Include(x => x.discussion).OrderBy(x => x.name).ToList());

        public IActionResult DeleteForum(int forumId)
        {
            try
            {
                forumService.DeleteForumFromDataBase(forumId);
                globalFacade.messageService.RemoveForumSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveForumError(TempData);
            }
            return RedirectToAction(nameof(ForumList));
        }

        public IActionResult DeleteSelectedForums(List<int> forumIds)
        {
            try
            {
                forumService.DeleteSelectedForumsFromDataBase(forumIds);
                globalFacade.messageService.RemoveForumSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.RemoveForumError(TempData);
            }
            return RedirectToAction(nameof(ForumList));
        }

        [HttpGet("/Forum/UpdateForum/{forumId}")]
        public IActionResult UpdateForum(int forumId)
        {
            Forum forum = forumService.FindForum(forumId);
            List<Discussion> discussions = globalFacade.db.discussions.OrderBy(x => x.name).ToList();
            return View(new ForumViewModel { forum = forum, discussions = discussions });
        }

        public IActionResult ConfirmUpdateForum(ForumViewModel forumViewModel)
        {
            try
            {
                forumService.ConfirmUpdateForumInDataBase(forumViewModel);
                globalFacade.messageService.UpdateForumSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.UpdateForumError(TempData);
            }
            return RedirectToAction(nameof(ForumList));
        }
    }
}
