using RestAPIDbQueryUpdate.Domain;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Repository.Interface
{
    public interface ILikeRepository : IRepositoryBase<Like>
    {
        Task<Like> FindByUserIdAndArticleId(string userId, string articleId);
    }
}
