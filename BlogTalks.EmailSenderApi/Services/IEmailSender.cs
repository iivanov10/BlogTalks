

using BlogTalks.EmailSenderAPI.DTO;

namespace BlogTalks.EmailSenderAPI.Services
{
    public interface IEmailSender
    {
        Task Send(EmailDTO request);
    }
}
