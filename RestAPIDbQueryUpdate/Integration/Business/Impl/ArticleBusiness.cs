using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Business.Interface;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Business.Impl
{
    public class ArticleBusiness : IArticleBusiness
    {
        IUserRepository _repository;
        IArticleRepository _articleRepository;

        public ArticleBusiness(IUserRepository repository, IArticleRepository articleRepository)
        {
            _repository = repository;
            _articleRepository = articleRepository;
        }

        public async Task<Article> NormalizeEntityAsync(Article article)
        {
            article.Author.WritableRelation = article.Author.Id.ToLong();

            var user = _repository.FindOneAsync(article.Author.WritableRelation).Result;

            article.Author.Id = user.Id;

            var articleDb = _articleRepository.FindOneAsync(article.WritableRelation).Result;
            if (articleDb != null && !string.IsNullOrEmpty(article.Id))
            {
                article.Id = articleDb.Id;
            }

            return article;
        }
    }
}
