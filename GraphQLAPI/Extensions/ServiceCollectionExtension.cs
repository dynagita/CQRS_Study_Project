using GraphQL;
using GraphQL.Server;
using GraphQLAPI.Query;
using GraphQLAPI.Repository.Context;
using GraphQLAPI.Repository.Impl;
using GraphQLAPI.Repository.Interface;
using GraphQLAPI.Scheme;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddGraphQL(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(opt => 
            {
                opt.AllowSynchronousIO = true;
            });

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            
            services.AddScoped<AppScheme>();

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        public static void AddContext(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<GraphQLAPIContext>(configs.GetSection(nameof(GraphQLAPIContext)));
            services.AddSingleton<IGraphQLAPIContext>(sp => sp.GetRequiredService<IOptions<GraphQLAPIContext>>().Value);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
        }
    }
}
