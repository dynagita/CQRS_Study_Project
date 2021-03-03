using Microsoft.Extensions.DependencyInjection;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using System;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler
{
    public class ReceiveHandlerFactory : IDisposable
    {

        IServiceProvider p;
        private bool _disposed;
        public string Entity { get; set; }
        public ReceiveHandlerFactory(IServiceProvider serviceProvider)
        {
            p = serviceProvider;
        }

        public IReceiveHandler CreateHandler()
        {
            if (string.IsNullOrEmpty(Entity))
            {
                throw new Exception("Please, set Entity property before user CreateHandler.");
            }

            switch (Entity.ToLower())
            {
                case "user":
                    return p.GetRequiredService<UserReceiveHandler>();
                case "article":
                    return p.GetRequiredService<ArticleReceiveHandler>();
                case "like":
                    return p.GetRequiredService<LikeReceiveHandler>();
                default:
                    throw new NotImplementedException($"Impossible create a handler for {Entity}. Please, implement this entity.");
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            p = null;
            Entity = null;
        }
    }
}
