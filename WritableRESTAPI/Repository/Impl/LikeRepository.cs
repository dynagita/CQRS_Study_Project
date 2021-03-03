using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Infrastructure.Context;
using WritableRESTAPI.Repository.Interface;

namespace WritableRESTAPI.Repository.Impl
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        IUserRepository _userRepository;
        IArticleRepository _articleRepository;
        public LikeRepository(WritableDbContext dbContext, IUserRepository userRepository, IArticleRepository articleRepository) : base(dbContext)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        public async Task<Like> GetByUserAndArticle(int userId, int articleId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.ArticleId == articleId);
        }

        protected override Like NormalizeForeignKeys(Like entity)
        {
            var user = _userRepository.Get(entity.UserId).Result;
            var article = _articleRepository.Get(entity.ArticleId).Result;
            entity.Article = article;
            entity.User = user;
            return base.NormalizeForeignKeys(entity);
        }

        public virtual async Task<Like> Update(int id, Like entity)
        {
            throw new NotSupportedException("You can't update a like. Operations permited: Include and Delete.");
        }
    }
}
