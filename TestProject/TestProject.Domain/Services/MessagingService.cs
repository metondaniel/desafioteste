using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Services;

namespace TestProject.Domain.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly PublisherClient _publisherClient;
        private readonly PubSubConfig _pubSubConfig;

        public MessagingService( PublisherClient publisherClient, IOptions<PubSubConfig> pubSubOptions )
        {
            _publisherClient = publisherClient;
            _pubSubConfig = pubSubOptions.Value;
        }

        public async Task PublishMessageAsync( string message )
        {
            var pubsubMessage = new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8( message )
            };
            await _publisherClient.PublishAsync( pubsubMessage );
        }
    }
}
