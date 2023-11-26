using shoes_final_exam.Models.MailModels;

namespace shoes_final_exam.Repositories
{
	public interface IEmailSender
	{
		void SendEmail(Message message);
		Task SendEmailAsync(Message message);
	}
}
