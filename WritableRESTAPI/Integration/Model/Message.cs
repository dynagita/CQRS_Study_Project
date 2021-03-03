using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Integration.Model
{
    public class Message
    {
        public string Entity { get; set; }

        public string Envelop { get; set; }

        public int Method { get; set; }
    }
}
