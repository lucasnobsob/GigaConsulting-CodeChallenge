using GigaConsulting.Domain.Services.Mail;

namespace GigaConsulting.Domain.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessage);
    }
}
