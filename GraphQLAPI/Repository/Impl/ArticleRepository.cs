using GraphQLAPI.Domain;
using GraphQLAPI.Repository.Context;
using GraphQLAPI.Repository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Repository.Impl
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(IGraphQLAPIContext context) : base(context)
        { 
        }

        protected override FilterDefinition<Article> GetDynamicFiler(Article entity)
        {
            FilterDefinition<Article> filter = null;

            if (!string.IsNullOrEmpty(entity.Id))
            {
                filter = Builders<Article>.Filter.Eq(x => x.Id, entity.Id);
            }

            if (!string.IsNullOrEmpty(entity.Abstract))
            {
                if (filter != null)
                {
                    filter = filter & "{Abstract: {$regex : /"+entity.Abstract+"/}}";
                }
                else
                {
                    filter = "{Abstract: {$regex : /" + entity.Abstract + "/}}";
                }
            }

            if (!string.IsNullOrEmpty(entity.Subject))
            {
                if (filter != null)
                {
                    filter = filter & "{Subject: {$regex : /" + entity.Subject + "/}}";
                }
                else
                {
                    filter = "{Subject: {$regex : /" + entity.Subject + "/}}";
                }
            }

            if (!string.IsNullOrEmpty(entity?.Author.Email))
            {
                if (filter != null)
                {
                    filter = filter & Builders<Article>.Filter.Eq(x => x.Author.Email, entity.Author.Email);
                }
                else
                {
                    filter = Builders<Article>.Filter.Eq(x => x.Author.Email, entity.Author.Email);
                }
            }
            
            return filter;
        }

    }
}
