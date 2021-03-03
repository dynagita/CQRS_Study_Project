using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Controllers
{
    public class UserController : ControllerBase<UserViewModel>
    {
        public UserController(IUserService service) : base(service)
        { 
        }
    }
}
