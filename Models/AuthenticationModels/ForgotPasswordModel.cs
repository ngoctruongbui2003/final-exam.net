using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models.AuthenticationModels
{
	public class ForgotPasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
