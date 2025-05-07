using Backend.DTO.EmailDTO;

namespace Backend.Interfaces.IEmail
{
    public interface IEmailService
    {
        void SendEmail(EmailResponse EmailData);
    }
}
