using Microsoft.Extensions.Logging;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Integration.Business.Interface;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl
{
    public class ArticleReceiveHandler : ReceiveHandler<Article>, IReceiveHandler
    {
        IArticleBusiness _business;
        public ArticleReceiveHandler(IArticleRepository repository, ILogger<Article> logger, IArticleBusiness business) : base(repository, logger)
        {
            _business = business;
        }

        protected override Article Normalize(Article entity)
        {
            entity = _business.NormalizeEntityAsync(entity).Result;

            return entity;
        }
    }
}
