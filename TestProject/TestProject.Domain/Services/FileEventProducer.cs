using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace TestProject.Domain.Services
{
    public class FileEventProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public FileEventProducer(string bootstrapServers, string topic)
        {
            _bootstrapServers = bootstrapServers;
            _topic = topic;
        }

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }

}
