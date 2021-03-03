using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Controllers
{
    public class ArticleController : ControllerBase<ArticleViewModel>
    {
        public ArticleController(IArticleService service) : base(service)
        { 
        }
    }
}
