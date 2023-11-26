using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models.AuthenticationModels
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string Address { set; get; }
        public DateTime? Birthday { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
