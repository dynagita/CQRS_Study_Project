using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Integration.Model;
using WritableRESTAPI.Repository.Interface;
using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.Util.Extensions;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Service.Impl
{
    public class ArticleService : ServiceBase<ArticleViewModel, Article>, IArticleService
    {        
        public ArticleService(IArticleRepository repository, IMapper mapper, IQueueSender sender, ILogger<ArticleViewModel> logger) : base(repository, mapper, sender, logger)
        {
        }
    }
}
