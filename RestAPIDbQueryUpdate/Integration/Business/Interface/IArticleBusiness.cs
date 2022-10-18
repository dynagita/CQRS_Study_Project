using RestAPIDbQueryUpdate.Domain;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Business.Interface
{
    public interface IArticleBusiness
    {
        Task<Article> NormalizeEntityAsync(Article article);
    }
}
