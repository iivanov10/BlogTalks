using BlogTalks.EmailSenderAPI.Models;

namespace BlogTalks.EmailSenderAPI.Services
{
    public interface IEmailSender
    {
        Task Send(EmailDTO request);
    }
}
