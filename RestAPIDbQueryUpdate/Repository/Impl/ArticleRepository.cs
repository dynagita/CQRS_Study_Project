using MongoDB.Driver;
using RestAPIDbQueryUpdate.Context;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Repository.Interface;

namespace RestAPIDbQueryUpdate.Repository.Impl
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        IUserRepository _userRepository;
        public ArticleRepository(IDbContext context, IUserRepository userRepository) : base(context)
        {
            _userRepository = userRepository;
        }

        protected override FilterDefinition<Article> GetEntityFilter(Article entity)
        {
            FilterDefinition<Article> filter = Builders<Article>.Filter.Eq(x => x.WritableRelation, entity.WritableRelation);

            return filter;
        }

        protected override UpdateDefinition<Article> GetUpdateDefinition(Article entity)
        {
            var update = Builders<Article>
                        .Update
                        .Set(x => x.Abstract, entity.Abstract)
                        .Set(x => x.Content, entity.Content)
                        .Set(x => x.Subject, entity.Subject)
                        .Set(x => x.Author, GetAuthor(entity.Author.WritableRelation))
                        .Set(x => x.TotalLike, entity.TotalLike)
                        .Set(x => x.WritableRelation, entity.WritableRelation);

            return update;
        }

        public User GetAuthor(long writableRelation)
        {
            var user = _userRepository.FindOneAsync(writableRelation).Result;
            return user;
        }
    }
}
