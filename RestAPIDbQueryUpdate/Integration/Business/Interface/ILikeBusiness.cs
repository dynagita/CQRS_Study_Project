using RestAPIDbQueryUpdate.Integration.Model;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.Business.Interface
{
    public interface ILikeBusiness
    {
        Task UpdateLike(Message message);
    }
}
