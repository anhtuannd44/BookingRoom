using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Ghi Nhớ Đăng Nhập")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
     
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Chưa đúng định dạng Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Mật Khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ Đăng Nhập?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Họ và Tên")]
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Địa Chỉ")]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Ngày Sinh")]
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date, ErrorMessage ="Bạn chưa nhập đúng ngày sinh, VD: 01/01/1997")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn Giới Tính")]
        [Display(Name = "Giới Tính")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Phone(ErrorMessage = "Bạn chưa nhập đúng Số Điện Thoại")]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Mật Khẩu")]
        [StringLength(100, ErrorMessage = "{0} tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận chưa chính xác")]
        public string ConfirmPassword { get; set; }
    }

    public class ProfileViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày Sinh")]
        public DateTime? Birthday { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận chưa chính xác")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
