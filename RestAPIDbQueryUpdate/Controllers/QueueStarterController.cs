using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class QueueStarterController : Controller
    {
        IQueueReader _reader;
        ILogger<QueueStarterController> _logger;

        public QueueStarterController(IQueueReader reader, ILogger<QueueStarterController> logger)
        {
            _reader = reader;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            try
            {
                await _reader.Read();
                
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(_reader.Read)}: Ex: {ex.AllMessages()} {Environment.NewLine} Stack: {ex.StackTrace}");
                return StatusCode(500);
            }
            
        }
    }
}
