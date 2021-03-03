using MongoDB.Driver;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Impl
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        IUserRepository _userRepository;
        IArticleRepository _articleRepository;
        public LikeRepository(IDbContext context, IUserRepository userRepository, IArticleRepository articleRepository) : base(context)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        protected override FilterDefinition<Like> GetEntityFilter(Like entity)
        {
            FilterDefinition<Like> filter = Builders<Like>.Filter.Eq(x => x.WritableRelation, entity.WritableRelation);

            return filter;
        }

        protected override UpdateDefinition<Like> GetUpdateDefinition(Like entity)
        {
            var update = Builders<Like>
                        .Update
                        .Set(x => x.ArticleId, GetArticleMongoID(entity.ArticleId.ToLong()))
                        .Set(x => x.UserId, GetUserMongoID(entity.UserId.ToLong()))
                        .Set(x => x.WritableRelation, entity.WritableRelation);

            return update;
        }

        private string GetUserMongoID(long writableRelation)
        {
            var user = _userRepository.FindOne(writableRelation).Result;
            return user.Id;
        }

        private string GetArticleMongoID(long writableRelation)
        {
            var article = _articleRepository.FindOne(writableRelation).Result;            
            return article.Id;
        }

        public async Task<Like> FindByUserIdAndArticleId(string userId, string articleId)
        {
                var filter = Builders<Like>.Filter.Eq(x => x.ArticleId, articleId);
                filter = filter & Builders<Like>.Filter.Eq(x => x.UserId, userId);

                return await _collection.Find<Like>(filter).FirstOrDefaultAsync();
        }
    }
}
