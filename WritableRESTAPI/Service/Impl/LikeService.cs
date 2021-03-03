using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Repository.Interface;
using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Service.Impl
{
    public class LikeService : ServiceBase<LikeViewModel, Like>, ILikeService
    {
        ILikeRepository _repository;
        public LikeService(ILikeRepository repository, IMapper mapper, IQueueSender sender, ILogger<LikeViewModel> logger) : base(repository, mapper, sender, logger)
        {
            _repository = repository;
        }

        protected override void Validate(Like entity)
        {
            var like = _repository.GetByUserAndArticle(entity.UserId, entity.ArticleId).Result;
            if (like != null && like.Id > 0 && like.Active)
            {
                throw new ApplicationException($"Like already exists and can't not be updated.");
            }
        }
    }
}
