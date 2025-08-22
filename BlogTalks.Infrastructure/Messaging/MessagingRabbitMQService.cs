using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;
using Microsoft.Extensions.Options;

namespace BlogTalks.Infrastructure.Messaging
{
    public class MessagingRabbitMQService : IMessagingService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;

        public MessagingRabbitMQService(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async Task Send(EmailDTO emailDto)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.RabbitURL,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password,
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: _rabbitMqSettings.ExchangeName,
                type: _rabbitMqSettings.ExchangeType,
                durable: true
            );

            await channel.QueueDeclareAsync(
                queue: _rabbitMqSettings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await channel.QueueBindAsync(
                queue: _rabbitMqSettings.QueueName,
                exchange: _rabbitMqSettings.ExchangeName,
                routingKey: _rabbitMqSettings.RouteKey
            );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailDto));

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: _rabbitMqSettings.ExchangeName,
                routingKey: _rabbitMqSettings.RouteKey,
                mandatory: false,
                basicProperties: properties,
                body: body
            );
        }
    }
}
