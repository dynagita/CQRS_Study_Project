using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Controllers
{
    public class LikeController : ControllerBase<LikeViewModel>
    {
        public LikeController(ILikeService service) : base(service)
        { 
        }
    }
}
