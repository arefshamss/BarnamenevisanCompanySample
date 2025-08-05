namespace BarnamenevisanCompany.Domain.Shared;

public static class SuccessMessages
{
    #region Common

    public const string SuccessfullyDone = "با موفقیت انجام شد!";

    public const string UpdateSuccessfullyDone = "ویرایش با موفقیت انجام شد.";

    public const string InsertSuccessfullyDone = "افزودن با موفقیت انجام شد.";

    public const string SavedChangesSuccessfully = "تغییرات با موفقیت اعمال شد";

    public const string DeleteSuccess = "عملیات حذف با موفقیت انجام شد";

    public const string RecoverSuccess = "عملیات بازگردانی با موفقیت انجام شد";

    public const string ChangeStatusSuccess = "تغییر وضعیت با موفقیت انجام شد";

    public const string ChangePasswordSuccess = "رمز عبور با موفقیت تغییر کرد";

    #endregion

    #region ContactUs

    public const string MessageSentSuccessfully = "پیام شما با موفقیت ارسال شد ";

    public const string EmailSentSuccessfully = "ایمیل با موفقیت ارسال شد ";

    #endregion

    #region User

    public const string LoginSuccessfullyDone = "ورود به حساب کاربری با موفقیت انجام شد";
    public const string RegisterSuccessfullyDone = "ثبت نام شما با موفقیت انجام شد، جهت فعالسازی حساب کاربری خود، کد ارسال شده را وارد کنید.";
    public const string OtpSentSuccessfullyDone = "کد تایید برای شما ارسال شد";
    public const string UserSuccessfullyActivated = "حساب کاربری شما با موفقیت فعال شد";
    public const string LogoutSuccessfullyDone = "با موفقیت از حساب کاربری خود خارج شدید";
    public const string UpdateAvatarSuccessfullyDone = "تصویر پروفایل با موفقیت تغییر کرد";  
    public const string DeleteAvatarSuccessfullyDone = "تصویر پروفایل با موفقیت حذف شد";  

    #endregion

    #region Sms

    public const string SmsSentSuccessfully = "پیامک با موفقیت برای شما ارسال شد";
    public const string SmsSentToUserSuccessfully = "پیامک با موفقیت ارسال شد";
    
    

    #endregion

    #region Order

    public const string SubmitOrderSuccessfullyDone = "سفارش شما ثبت شد و پس از بررسی به شما اطلاع رسانی خواهد شد.";

    #endregion

    #region Consult

    public const string ConsultSmsSentSuccessfully = "پیامک برای اطلاع رسانی کاربر ارسال شد";
    
    public const string InsertConsultSuccessfully = "درخواست مشاوره شما ثبت شد و پس از بررسی به شما اطلاع رسانی خواهد شد";


    #endregion
}

public static class ErrorMessages
{
    #region Common

    public const string NotValid = "{0} وارد شده معتبر نمی باشد";
    public const string MobileNotValid = "شماره همراه وارد شده معتبر نمی باشد"; 

    public const string MaxLengthError = "تعداد کاراکتر مجاز {1} عدد می باشد";

    public const string MinLengthError = "تعداد حداقل کاراکتر {1} عدد می باشد";

    public const string RequiredError = "وارد کردن {0} الزامی می باشد";

    public const string NotFoundError = "موردی یافت نشد";
    public const string RangeError = "بازه انتخابی باید بین {1} تا {2} باشد";
    

    public const string OperationFailedError = "عملیات شکست خورد";

    public const string BadRequestError = "درخواست شما نامعتبر می باشد";

    public const string ModelStateNotValid = "اطلاعات وارد شده نامعتبر می باشد";

    public const string SomethingWentWrong = "مشکلی پیش آمده است";

    public const string AlreadyExistError = "{0} از قبل موجود است";

    public const string NullValue = "اطلاعاتی یافت نشد";

    public const string CaptchaError = "خطای اعتبارسنجی! لطفا مجدد تلاش نمایید.";
    public const string AccessDenied = "شما دسترسی ندارید";
    
    public const string EmailSendError = "در فرایند ارسال ایمیل مشکلی پیش آمده";
    
    

    #endregion

    #region Files

    public const string FileFormatError = "فرمت فایل آپلود شده معتبر نمی باشد";
    
    public const string FileArchiveFormatError = "لطفا فایل با پسوند zip یا rar بارگزاری نمایید.";

    public const string ImageIsRequired = "وارد کردن تصویر الزامی میباشد";

    public const string FileNotFound = "فایل یافت نشد";

    #endregion

    #region User

    public const string ConflictError = "{0} وارد شده تکراری می باشد";

    public const string UserNotFoundError = "کاربری با مشخصات وارد شده یافت نشد";

    public const string UserNotActiveError = "حساب کاربری شما غیرفعال می باشد";

    public const string ExpireConfirmCodeError = "کد تایید وارد شده منقضی شده است";

    public const string InvalidConfirmationCode = "کد تایید وارد شده نامعتبر می باشد";

    public const string PasswordNotCorrect = "رمز عبور وارد شده صحیح نمی باشد";

    public const string PasswordCompareError = "تکرار رمز عبور با رمز عبور وارد شده مطابقت ندارد";

    public const string UserIsActive = "حساب کاربری شما فعال می باشد";
    
    public const string RequiredUserFullName = "لطفا قبل از {0}، اطلاعات پروفایل خود را تکمیل نمایید.";

    public const string ActiveCodeExpireDateTime = "برای ارسال مجدد کد تایید باید از ارسال قبلی حداقل 90 ثانیه گذشته باشد";
    
    public const string ConflictActiveUserError = "این شماره موبایل بصورت فعال در لیست کاربران موجود است و امکان {0} وجود ندارد.";

    #region UserPosition

    public const string PriorityExist = "این اولویت نمایش برای کاربر دیگر ثبت شده است";

    public const string AlreadyAdded = "این کاربر از قبل سمتی براش مشخص شده است";

    #endregion

    #endregion

    #region ContactUs

    public const string AlreadyAnsweredThisMessage = "به این پیام قبلا پاسخ داده شده است";

    #endregion

    #region Sms

    public const string SmsDidNotSendError = "خطایی در فرآیند ارسال پیامک رخ داد";

    #endregion

    #region Order

    public const string RequiredOrderAnswer = "پاسخ به سفارش نمیتواند خالی باشد";
    public const string PriorityOrderStepExist = "این اولویت نمایش از قبل موجود است";
    
    #endregion

    #region Consult

    public const string ConsultEmptyUserFullName = "وارد کردن نام و نام خانوداگی برای ثبت مشاوره الزامیست";
    public const string ConsultNotAuthenticateUser = "برای ثبت درخواست مشاوره باید اول ثبت نام کنید";
    


    #endregion

    #region Ticket

    public const string TicketIsClosed = "این تیکت بسته شده است";

    #endregion
    
    #region JobUserMapping
    
    public const string AlreadyJobSubmitError = "شما قبلاً برای این موقعیت شغلی درخواست داده‌اید و امکان درخواست مجدد ندارید";
    
    #endregion

    #region Project

    public const string ProjectPriorityExist = "این اولویت نمایش از قبل برای پروژه دیگر ثبت شده است";

    #endregion
}