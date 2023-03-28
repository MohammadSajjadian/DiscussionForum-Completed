using Microsoft.AspNetCore.Mvc;

namespace Discussion_Forum.Controllers
{
    public class CustomController : Controller
    {
        #region Statuses
        public const string success = "success";
        public const string error = "error";
        public const string warning = "warning";
        public const string info = "info";
        #endregion

        #region Action&Controllers
        public const string index = "Index";
        public const string homeControllerName = "Home";
        public const string RegisterControllerName = "Register";
        public const string LoginControllerName = "Login";
        #endregion

        #region Messages
        #region Login
        public const string userNotFound = "نام کاربری موجود نمیباشد";
        public const string lockedOutMessage = "اکانت شما به دلیل اشتباهات متعدد در رمز عبور به مدت 1 دقیقه مسدود خواهد شد. لطفا بعدا امتحان کنید";
        public const string loginError = "نام کاربری یا رمز عبور نادرست است";
        #endregion

        #region Register
        public const string userExist = "کاربر در حال حاظر موجود میباشد";
        public const string registerErrorMessage = "فرمت نام کاربری یا رمز عبور نادرست است";
        public const string emailConfirmationSubject = "تایید نام کاربری";
        public const string emailConfirmationExpire = "لینک فعالسازی ایمیل منقضی شده است. لطفا دوباره تلاش کنید";
        public const string emailConfirmed = "حساب کاربری شما با موفقیت فعال شد";
        public const string emailConfirmationFailed = "فعال سازی حساب کاربری با شکست مواجه شد، لطفا دوباره امتحان کنید";
        public const string emailConfirmationMessage = "ایمیل تایید نام کاربری برای شما ارسال شد";
        #endregion
        #endregion
    }
}
