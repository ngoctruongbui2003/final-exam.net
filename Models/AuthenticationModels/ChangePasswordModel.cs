using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models.AuthenticationModels
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string PasswordCurrent { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
