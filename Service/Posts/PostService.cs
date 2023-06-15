using Data.Entities;
using Data.ViewModels;
using Service.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Posts
{
    public class PostService : IPost
    {
        private readonly GlobalFacade globalFacade;
        public PostService(GlobalFacade globalFacade)
        {
            this.globalFacade = globalFacade;
        }

        public void AddPostToDataBase(string description, int topicId, ApplicationUser user)
        {
            Post post = new() { description = description, userId = user.Id, topicId = topicId, createTime = DateTime.Now };
            globalFacade.db.Add(post);
            globalFacade.db.SaveChanges();
        }

        public void ConfirmUpdatePostInDataBase(PostViewModel postViewModel)
        {
            Post post = FindPost(postViewModel.post.id);
            post.description = post.description;

            globalFacade.db.Update(post);
            globalFacade.db.SaveChanges();
        }

        public void RemoveLikeFromDataBase(string userId, int postId)
        {
            Like like = globalFacade.db.likes.Single(x => x.userId == userId && x.postId == postId);

            globalFacade.db.Remove(like);
            globalFacade.db.SaveChanges();
        }

        public void AddLikeToDataBase(string userId, int postId)
        {
            Like like = new() { userId = userId, postId = postId };

            globalFacade.db.Add(like);
            globalFacade.db.SaveChanges();
        }
        
        public Post FindPost(int postId) => globalFacade.db.posts.Find(postId);
        
        public bool IsUserLiked(string userId, int postId) => globalFacade.db.likes.Any(x => x.userId == userId && x.postId == postId);

        public int LikesCount(int postId) => globalFacade.db.likes.Where(x => x.postId == postId).Count();
    }
}
