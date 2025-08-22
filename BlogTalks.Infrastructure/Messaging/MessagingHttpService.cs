using System.Net.Http.Json;
using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;

namespace BlogTalks.Infrastructure.Messaging
{
    public class MessagingHttpService : IMessagingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessagingHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Send(EmailDTO email)
        {
            var client = _httpClientFactory.CreateClient("EmailSenderAPI");
            await client.PostAsJsonAsync("/email", email);
        }
    }
}
