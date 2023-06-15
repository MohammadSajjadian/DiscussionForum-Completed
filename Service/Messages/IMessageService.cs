using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Service.Messages
{
    public interface IMessageService
    {
        void LockedOutTrue(ITempDataDictionary TempData);
        void LoginError(ITempDataDictionary TempData);
        void UserNotFound(ITempDataDictionary TempData);
        void RegisterError(ITempDataDictionary TempData);
        void UserExist(ITempDataDictionary TempData);
        void EmailConfirmation(ITempDataDictionary TempData);
        void EmailConfirmed(ITempDataDictionary TempData);
        void EmailConfirmationFailed(ITempDataDictionary TempData);
        void EmailConfirmationExpire(ITempDataDictionary TempData);

        #region Discussion
        void AddDiscussionSucceeded(ITempDataDictionary TempData);
        void AddDiscussionError(ITempDataDictionary TempData);
        void RemoveDiscussionSucceeded(ITempDataDictionary TempData);
        void RemoveDiscussionError(ITempDataDictionary TempData);
        void UpdateDiscussionSucceeded(ITempDataDictionary TempData);
        void UpdateDiscussionError(ITempDataDictionary TempData);
        #endregion

        #region Forum
        void AddForumSucceeded(ITempDataDictionary TempData);
        void AddForumError(ITempDataDictionary TempData);
        void RemoveForumSucceeded(ITempDataDictionary TempData);
        void RemoveForumError(ITempDataDictionary TempData);
        void UpdateForumSucceeded(ITempDataDictionary TempData);
        void UpdateForumError(ITempDataDictionary TempData);
        #endregion

        #region Topic
        void AddTopicSucceeded(ITempDataDictionary TempData);
        void AddTopicError(ITempDataDictionary TempData);
        void RemoveTopicSucceeded(ITempDataDictionary TempData);
        void RemoveTopicError(ITempDataDictionary TempData);
        void UpdateTopicSucceeded(ITempDataDictionary TempData);
        void UpdateTopicError(ITempDataDictionary TempData);
        #endregion

        #region Post
        void AddPostSucceeded(ITempDataDictionary TempData);
        void AddPostError(ITempDataDictionary TempData);
        void RemovePostSucceeded(ITempDataDictionary TempData);
        void RemovePostError(ITempDataDictionary TempData);
        void UpdatePostSucceeded(ITempDataDictionary TempData);
        void UpdatePostError(ITempDataDictionary TempData);
        #endregion

        #region Profile
        void UpdateProfileSucceeded(ITempDataDictionary TempData);
        void PasswordChangeSucceeded(ITempDataDictionary TempData);
        void PasswordChangeError(ITempDataDictionary TempData);
        #endregion
    }
}
