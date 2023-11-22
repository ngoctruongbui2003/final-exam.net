﻿using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace shoes_final_exam.Mail
{
	public class SendMailService : IEmailSender
	{
		private readonly MailSettings _mailSettings;
		private readonly ILogger<SendMailService> _logger;

		public SendMailService(MailSettings mailSettings,
							   ILogger<SendMailService> logger) 
		{
			_mailSettings = mailSettings;
			_logger = logger;
			_logger.LogInformation("Create SendMailService");
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage();
			message.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
			message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
			message.To.Add(MailboxAddress.Parse(email));
			message.Subject = subject;

			var builder = new BodyBuilder();
			builder.HtmlBody = htmlMessage;
			message.Body = builder.ToMessageBody();

			// dùng SmtpClient của MailKit
			using var smtp = new MailKit.Net.Smtp.SmtpClient();

			try
			{
				smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
				smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
				await smtp.SendAsync(message);
			}
			catch (Exception ex)
			{
				// Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
				System.IO.Directory.CreateDirectory("mailssave");
				var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
				await message.WriteToAsync(emailsavefile);

				_logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
				_logger.LogError(ex.Message);
			}

			smtp.Disconnect(true);

			_logger.LogInformation("send mail to: " + email);
		}
	}
}
