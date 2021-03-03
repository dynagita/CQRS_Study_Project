using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;

namespace WritableRESTAPI.Repository.Interface
{
    public interface ILikeRepository : IRepositoryBase<Like>
    {
        Task<Like> GetByUserAndArticle(int userId, int articleId);
    }
}
