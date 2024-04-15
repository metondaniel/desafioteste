using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class PubSubConfig
    {
        public string ProjectId { get; set; }
        public string TopicId { get; set; }
        public string SubscriptionId { get; set; }
    }
}
