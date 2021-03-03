using Newtonsoft.Json;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Business.Interface;
using RestAPIDbQueryUpdate.Integration.Model;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace RestAPIDbQueryUpdate.Integration.Business.Impl
{
    public class LikeBusiness : ILikeBusiness
    {
        IArticleRepository _articleRepository;
        IUserRepository _userRepository;
        ILikeRepository _repository;
        public LikeBusiness(IArticleRepository articleRepository, IUserRepository userRepository, ILikeRepository repository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task UpdateLike(Message message)
        {
            QueueMethod method = (QueueMethod)message.Method;

            Like like = JsonConvert.DeserializeObject<Like>(message.Envelop);

            User user = _userRepository.FindOne(like.UserId.ToLong()).Result;

            Article article = _articleRepository.FindOne(like.ArticleId.ToLong()).Result;


            using (TransactionScope scope = new TransactionScope())
            {
                switch (method)
                {
                    case QueueMethod.Insert:
                        InsertLike(user.Id, article.Id, like.WritableRelation);
                        UpdateArticleTotalCount(article);
                        break;
                    case QueueMethod.Update:
                        throw new NotSupportedException($"Like feature does not support update.");
                    case QueueMethod.Delete:
                        DeleteLike(user.Id, article.Id);
                        UpdateArticleTotalCount(article, false);
                        break;
                    default:
                        throw new NotImplementedException($"Method {method.ToString()} doesn't have an implementation.");
                }

                scope.Complete();
            }

        }

        private bool LikeExists(string userId, string articleId)
        {
            var likeDb = _repository.FindByUserIdAndArticleId(userId, articleId).Result;
            if (likeDb != null && !string.IsNullOrEmpty(likeDb.Id))
            {
                return true;
            }

            return false;
        }

        private void InsertLike(string userId, string articleId, long writableRelation)
        {
            if (!LikeExists(userId, articleId))
            {
                Like like = new Like()
                {
                    ArticleId = articleId,
                    UserId = userId,
                    WritableRelation = writableRelation
                };

                _repository.Insert(like).GetAwaiter().GetResult();
            }
            else
            {
                throw new NotSupportedException("A user can not like an article more than once.");
            }
        }

        private void UpdateArticleTotalCount(Article article, bool increment = true)
        {
            if (increment)
            {
                article.TotalLike++;
            }
            else
            {
                article.TotalLike--;
            }
            
            _articleRepository.Update(article).GetAwaiter().GetResult();
        }

        private void DeleteLike(string userId, string articleId)
        {
            var likeDb = _repository.FindByUserIdAndArticleId(userId, articleId).Result;
            if (likeDb != null && !string.IsNullOrEmpty(likeDb.Id))
            {
                _repository.Delete(likeDb).GetAwaiter().GetResult();
            }
            else
            {
                throw new Exception($"Like UserId({userId}) && ArticleId({articleId}) was not found.");
            }
        }
    }
}
