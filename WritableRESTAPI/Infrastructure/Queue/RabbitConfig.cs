using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Infrastructure.Queue
{
    public class RabbitConfig
    {
        public string Connection { get; set; }

        public string QueueName { get; set; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; }

        public bool AutoDelete { get; set; }
    }
}
