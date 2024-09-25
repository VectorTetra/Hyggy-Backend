using HyggyBackend.BLL.Services.EmailService;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IEmailSender
	{
		void SendEmail(Message message);
	}
}
