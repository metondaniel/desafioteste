using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace TestProject.Domain.Services
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }

        // Optional: Define other global Kafka settings here

        public ProducerConfig ProducerConfig { get; set; }
        public ConsumerConfig ConsumerConfig { get; set; }
    }
}
