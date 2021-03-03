using GraphQLAPI.Domain;
using GraphQLAPI.Repository.Context;
using GraphQLAPI.Repository.Interface;
using MongoDB.Driver;

namespace GraphQLAPI.Repository.Impl
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(IGraphQLAPIContext context) : base(context)
        { 
        }

        protected override FilterDefinition<Like> GetDynamicFiler(Like entity)
        {
            FilterDefinition<Like> filter = null;

            if (!string.IsNullOrEmpty(entity.Id))
            {
                filter = Builders<Like>.Filter.Eq(x => x.Id, entity.Id);
            }

            if (!string.IsNullOrEmpty(entity.UserId))
            {
                if (filter != null)
                {
                    filter = filter & Builders<Like>.Filter.Eq(x => x.UserId, entity.UserId);
                }
                else
                {
                    filter = Builders<Like>.Filter.Eq(x => x.UserId, entity.UserId);
                }
            }

            if (!string.IsNullOrEmpty(entity.ArticleId))
            {
                if (filter != null)
                {
                    filter = filter & Builders<Like>.Filter.Eq(x => x.ArticleId, entity.ArticleId);
                }
                else
                {
                    filter = Builders<Like>.Filter.Eq(x => x.ArticleId, entity.ArticleId);
                }
            }


            return filter;
        }

    }
}
