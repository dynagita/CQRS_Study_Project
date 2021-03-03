using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Context
{
    public class GraphQLAPIContext : IGraphQLAPIContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
