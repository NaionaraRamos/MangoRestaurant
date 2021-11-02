using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using Azure.Messaging.ServiceBus;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessage : IMessageBus
    {
        private string connectionString = "pegar da azure";
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            //ISenderClient senderClient = new TopicClient(connectionString, topicName);
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            //var finalMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            //await senderClient.SendAsync(finalMessage);
            await sender.SendMessageAsync(finalMessage);
            //await senderClient.CloseAsync();
            await client.DisposeAsync();
        }
    }
}
