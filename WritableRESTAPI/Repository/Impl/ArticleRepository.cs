using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Infrastructure.Context;
using WritableRESTAPI.Repository.Interface;

namespace WritableRESTAPI.Repository.Impl
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        IUserRepository _userRepository;
        public ArticleRepository(WritableDbContext context, IUserRepository userRepository) : base(context)
        {
            _userRepository = userRepository;
        }

        protected override Article NormalizeForeignKeys(Article entity)
        {
            var user = _userRepository.GetUserByMail(entity.Author.Email);
            entity.Author = user;

            return entity;
        }
    }
}
