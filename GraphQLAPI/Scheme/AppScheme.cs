using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Query;

namespace GraphQLAPI.Scheme
{
    public class AppScheme : Schema
    {
        public AppScheme(IDependencyResolver provider) : base(provider)
        {
            Query = provider.Resolve<AppQuery>();
        }
    }
}
