using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQLAPI.Scheme;
using Microsoft.AspNetCore.Builder;

namespace GraphQLAPI.Extensions
{
    public static class ApplicationExtension
    {

        public static void AddGraphQL(this IApplicationBuilder app)
        {
            app.UseGraphQL<AppScheme>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
        }

    }
}
