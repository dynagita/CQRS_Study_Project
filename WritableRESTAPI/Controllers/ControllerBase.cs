using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WritableRESTAPI.Service.Interface;

namespace WritableRESTAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControllerBase<ViewModel> : Microsoft.AspNetCore.Mvc.Controller
    {
        protected IServiceBase<ViewModel> Service { get; private set; }

        public ControllerBase(IServiceBase<ViewModel> service)
        {
            Service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<ViewModel>> Insert(ViewModel viewModel)
        {
            try
            {
                return await Service.InsertAsync(viewModel);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<ViewModel>> Update(int id, ViewModel viewModel)
        {
            try
            {
                return await Service.UpdateAsync(id, viewModel);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<ViewModel>> Delete(int id)
        {
            try
            {
                return await Service.DeleteAsync(id);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
