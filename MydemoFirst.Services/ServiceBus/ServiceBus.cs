using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using MydemoFirst.Services.Contracts;
using System.Text.Json;

namespace MydemoFirst.Services.ServiceBus
{
    public class ServiceBus<T> : IServiceBus<T> where T : class
    {
        private readonly ServiceBusClient _serviceBus;

        public ServiceBus(IAzureClientFactory<ServiceBusClient> serviceBusFactory)
        {

            _serviceBus = serviceBusFactory.CreateClient("NameSpace");
        }



        public async Task Send(string topicName, T entity, Dictionary<string, string> filters = null)
        {
            var sender = _serviceBus.CreateSender(topicName);

            var body = JsonSerializer.Serialize(entity);
            var message = new ServiceBusMessage(body);
            message.TimeToLive = TimeSpan.FromDays(1);

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    message.ApplicationProperties.Add(filter.Key, filter.Value);

                }
            }


            await sender.SendMessageAsync(message);
        }



        public async Task<T> Recevie(string topicName)
        {

            ServiceBusReceiver receiver = _serviceBus.CreateReceiver(topicName);

            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

            string body = receivedMessage.Body.ToString();

            var results = JsonSerializer.Deserialize<T>(body);

            await receiver.CompleteMessageAsync(receivedMessage);

            return results;

        }


        //public async Task Recevie(string topicName,)
        //{
        //    ServiceBusReceiver receiver = _serviceBus.CreateReceiver(topicName);

        //    ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
        //    T objectRecevie = JsonSerializer.Deserialize<T>(body);

        //    receiver.CompleteMessageAsync(receivedMessage);
        //}


    }
}
