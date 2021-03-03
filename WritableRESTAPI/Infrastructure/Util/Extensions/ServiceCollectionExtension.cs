using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WritableRESTAPI.Infrastructure.Context;
using WritableRESTAPI.Repository.Interface;
using WritableRESTAPI.Repository.Impl;
using WritableRESTAPI.Service.Impl;
using WritableRESTAPI.Service.Interface;
using Microsoft.OpenApi.Models;
using System;
using WritableRESTAPI.Infrastructure.Profiles;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Integration.Impl;
using Microsoft.Extensions.Logging;
using WritableRESTAPI.Infrastructure.Queue;

namespace WritableRESTAPI.Util.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddContext(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<WritableDbContext>(opt => opt.UseMySql(config.GetConnectionString("WritableDbContext")));
            service.AddScoped<RabbitConfig>();
        }

        public static void AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IArticleRepository, ArticleRepository>();
            service.AddScoped<ILikeRepository, LikeRepository>();
        }

        public static void AddServices(this IServiceCollection service)
        {
            service.AddSingleton<ILoggerFactory, LoggerFactory>();
            service.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IArticleService, ArticleService>();
            service.AddScoped<ILikeService, LikeService>();
            service.AddSingleton<IQueueSender, QueueSender>();
        }

        public static void AddAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserProfile));
            service.AddAutoMapper(typeof(ArticleProfile));
            service.AddAutoMapper(typeof(LikeProfile));
        }

        public static void AddSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen((c) => 
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "WritableAPI Test",
                        Version = "v1",
                        Description = "This is a project created to be a Writable API, used for create and test a CQRS implementation.",
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
