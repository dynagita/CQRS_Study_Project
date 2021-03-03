using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Context
{
    public interface IGraphQLAPIContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
