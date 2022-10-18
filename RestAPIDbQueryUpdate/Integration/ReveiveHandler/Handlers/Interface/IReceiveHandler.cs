using RestAPIDbQueryUpdate.Integration.Model;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface
{
    public interface IReceiveHandler
    {
        Task HandleAsync(Message message);
    }
}
