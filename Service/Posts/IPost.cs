using Data.Entities;
using Data.ViewModels;

namespace Service.Posts
{
    public interface IPost
    {
        void AddPostToDataBase(string description, int topicId, ApplicationUser user);
        void ConfirmUpdatePostInDataBase(PostViewModel postViewModel);
        Post FindPost(int postId);
        bool IsUserLiked(string userId, int postId);
        void RemoveLikeFromDataBase(string userId, int postId);
        void AddLikeToDataBase(string userId, int postId);
        int LikesCount(int postId);
    }
}
