using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Type
{
    public class AppType
    {
        public string Query { get; set; }

        public ResolveFieldContext<object> MyProperty { get; set; }
    }
}
