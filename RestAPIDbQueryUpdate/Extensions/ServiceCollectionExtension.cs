using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Integration.Business.Impl;
using RestAPIDbQueryUpdate.Integration.Business.Interface;
using RestAPIDbQueryUpdate.Integration.Impl;
using RestAPIDbQueryUpdate.Integration.Interface;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using RestAPIDbQueryUpdate.Integration.Service.Interface;
using RestAPIDbQueryUpdate.Profiles;
using RestAPIDbQueryUpdate.Repository.Impl;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<ILikeBusiness, LikeBusiness>();
            services.AddTransient<IArticleBusiness, ArticleBusiness>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IQueueReader, QueueReader>();
        }

        public static void AddContext(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<DbContext>(configs.GetSection(nameof(DbContext)));
            services.AddSingleton<IDbContext>(sp => sp.GetRequiredService<IOptions<DbContext>>().Value);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ReceiveHandlerFactory>();

            services.AddSingleton<UserReceiveHandler>()
                    .AddSingleton<IReceiveHandler, UserReceiveHandler>(s => s.GetService<UserReceiveHandler>());

            services.AddSingleton<LikeReceiveHandler>()
                    .AddSingleton<IReceiveHandler, LikeReceiveHandler>(s => s.GetService<LikeReceiveHandler>());

            services.AddSingleton<ArticleReceiveHandler>()
                    .AddSingleton<IReceiveHandler, ArticleReceiveHandler>(s => s.GetService<ArticleReceiveHandler>());
        }

        public static void AddAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserProfile));
        }

        public static void AddSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen((c) =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "RESTAPI DbQueryUpdate",
                        Version = "v1",
                        Description = "This is a project created to update dbQuery when a change occurs into WritableDB.",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Yanagita",
                            Url = new Uri("https://www.linkedin.com/in/daniel-yanagita-88860770/"),
                            Email = "dynagita@gmail.com"
                        }
                    });
            });
        }
    }
}
