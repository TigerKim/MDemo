using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PartyInvites.Models
{

    #region 모델
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "새 암호와 확인 암호가 일치하지 않습니다.")]
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("현재 암호")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("새 암호")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("새 암호 확인")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("사용자 이름")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("암호")]
        public string Password { get; set; }

        [DisplayName("사용자 이름 및 암호 저장")]
        public bool RememberMe { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "암호와 확인 암호가 일치하지 않습니다.")]
    public class RegisterModel
    {
        [Required]
        [DisplayName("사용자 이름")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("전자 메일 주소")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("암호")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("암호 확인")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region Services
    // FormsAuthentication 형식이 봉인되어 있고 정적 멤버를 포함하므로 해당 멤버를 호출하는
    // 코드를 단위 테스트하기 어렵습니다. 아래의 인터페이스 및 도우미 클래스는
    // AccountController 코드를 단위 테스트할 수 있도록 하기 위해 이러한 형식에 추상 래퍼를 만드는
    // 방법을 보여 줍니다.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "newPassword");

            // 기본 ChangePassword()는 특정 실패 시나리오에서 false를 반환하지 않고
            // 예외를 throw합니다.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("값은 null이거나 비어 있을 수 없습니다.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 전체 상태 코드 목록은 http://go.microsoft.com/fwlink/?LinkID=177550을
            // 참조하십시오.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "사용자 이름이 이미 있습니다. 다른 사용자 이름을 입력하십시오.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "해당 전자 메일 주소를 가진 사용자 이름이 이미 있습니다. 다른 전자 메일 주소를 입력하십시오.";

                case MembershipCreateStatus.InvalidPassword:
                    return "제공한 암호가 잘못되었습니다. 올바른 암호 값을 입력하십시오.";

                case MembershipCreateStatus.InvalidEmail:
                    return "제공한 전자 메일 주소가 잘못되었습니다. 값을 확인한 후 다시 시도하십시오.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "제공한 암호 찾기 대답이 잘못되었습니다. 값을 확인한 후 다시 시도하십시오.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "제공한 암호 찾기 질문이 잘못되었습니다. 값을 확인한 후 다시 시도하십시오.";

                case MembershipCreateStatus.InvalidUserName:
                    return "제공한 사용자 이름이 잘못되었습니다. 값을 확인한 후 다시 시도하십시오.";

                case MembershipCreateStatus.ProviderError:
                    return "인증 공급자가 오류를 반환했습니다. 입력한 내용을 확인하고 다시 시도하십시오. 문제가 계속되면 시스템 관리자에게 문의하십시오.";

                case MembershipCreateStatus.UserRejected:
                    return "사용자 생성 요청이 취소되었습니다. 입력한 내용을 확인하고 다시 시도하십시오. 문제가 계속되면 시스템 관리자에게 문의하십시오.";

                default:
                    return "알 수 없는 오류가 발생했습니다. 입력한 내용을 확인하고 다시 시도하십시오. 문제가 계속되면 시스템 관리자에게 문의하십시오.";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}'과(와) '{1}'이(가) 일치하지 않습니다.";
        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}'은(는) {1}자 이상이어야 합니다.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
    #endregion

}
