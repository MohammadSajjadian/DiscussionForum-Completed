using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Messages
{
    public class MessageService : IMessageService
    {
        public enum Status
        {
            success,
            error,
            info,
            warning
        }

        #region Login
        public void LockedOutTrue(ITempDataDictionary TempData)
        {
            TempData[Status.warning.ToString()] = "Your account will be blocked for one minute. Please try again later";
        }

        public void LoginError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Username or password is incorrect";
        }

        public void UserNotFound(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Username is not available";
        }
        #endregion

        #region Register
        public void RegisterError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Username or password is incorrect";
        }

        public void UserExist(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Username is already exist";
        }

        public void EmailConfirmation(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Email confirmation sended to you";
        }

        public void EmailConfirmed(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Your account successfully activated";
        }

        public void EmailConfirmationFailed(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Account activation failed, Please try again";
        }

        public void EmailConfirmationExpire(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Email activation link has expired. Please try again";
        }
        #endregion

        #region Discussion
        public void AddDiscussionSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Discussion added successfully";
        }

        public void AddDiscussionError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to add discussion";
        }

        public void RemoveDiscussionSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Discussion removed successfully";
        }

        public void RemoveDiscussionError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to remove discussion";
        }

        public void UpdateDiscussionSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Discussion updated successfully";
        }

        public void UpdateDiscussionError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to update discussion";
        }
        #endregion

        #region Forum
        public void AddForumSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Forum added successfully";
        }

        public void AddForumError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to add forum";
        }

        public void RemoveForumSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Forum removed successfully";
        }

        public void RemoveForumError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to remove forum";
        }

        public void UpdateForumSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Forum updated successfully";
        }

        public void UpdateForumError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to update forum";
        }
        #endregion

        #region Topic
        public void AddTopicSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Topic added successfully";
        }

        public void AddTopicError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to add topic";
        }

        public void RemoveTopicSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Topic removed successfully";
        }

        public void RemoveTopicError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to remove topic";
        }

        public void UpdateTopicSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Topic updated successfully";
        }

        public void UpdateTopicError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to update topic";
        }
        #endregion

        #region Post
        public void AddPostSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Post added successfully";
        }

        public void AddPostError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to add post";
        }

        public void RemovePostSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Post removed successfully";
        }

        public void RemovePostError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to remove post";
        }

        public void UpdatePostSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Post updated successfully";
        }

        public void UpdatePostError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to update post";
        }
        #endregion

        #region Profile
        public void UpdateProfileSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Profile updated successfully";
        }

        public void PasswordChangeSucceeded(ITempDataDictionary TempData)
        {
            TempData[Status.success.ToString()] = "Password Changed successfully";
        }

        public void PasswordChangeError(ITempDataDictionary TempData)
        {
            TempData[Status.error.ToString()] = "Failed to change password";
        }
        #endregion
    }
}

