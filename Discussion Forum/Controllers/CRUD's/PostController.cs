using Data.Entities;
using Data.ViewModels;
using Discussion_Forum.Controllers.Account;
using Discussion_Topic.Controllers.CRUD_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Facades;
using Service.Posts;

namespace Discussion_Forum.Controllers.CRUD_s
{
    [Authorize]
    public class PostController : Controller
    {
        #region Dependency Injections
        private readonly GlobalFacade globalFacade;
        private readonly IPost postServices;
        public PostController(GlobalFacade globalFacade, IPost postServices)
        {
            this.globalFacade = globalFacade;
            this.postServices = postServices;
        }
        #endregion

        public async Task<IActionResult> ConfirmAddPost(string description, int topicId, string topicName)
        {
            try
            {
                ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);
                postServices.AddPostToDataBase(description, topicId, user);
                globalFacade.messageService.AddPostSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.AddPostError(TempData);
            }
            return RedirectToAction(nameof(TopicController.Index), nameof(TopicController).Replace("Controller", ""), new { topicId, topicName });
        }

        [HttpGet("/Post/UpdatePost/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);
            Post post = postServices.FindPost(postId);
            if (user.Id != post.userId)
            {
                return BadRequest();
            }

            return View(new PostViewModel { post = post });
        }

        public IActionResult ConfirmUpdatePost(PostViewModel postViewModel)
        {
            try
            {
                postServices.ConfirmUpdatePostInDataBase(postViewModel);
                globalFacade.messageService.UpdatePostSucceeded(TempData);
            }
            catch
            {
                globalFacade.messageService.UpdatePostError(TempData);
            }
            return RedirectToAction(nameof(ProfileController.Index), nameof(ProfileController).Replace("Controller", ""));
        }

        public async Task<IActionResult> LikeOptions(int postId)
        {
            ApplicationUser user = await globalFacade.userManager.FindByNameAsync(User.Identity.Name);

            bool status;
            if (postServices.IsUserLiked(user.Id, postId))
            {
                postServices.RemoveLikeFromDataBase(user.Id, postId);
                status = false;
            }
            else
            {
                postServices.AddLikeToDataBase(user.Id, postId);
                status = true;
            }
            return Json(new { status, likes = postServices.LikesCount(postId) });
        }
    }
}
