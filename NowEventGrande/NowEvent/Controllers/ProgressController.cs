using Microsoft.AspNetCore.Mvc;
using NowEvent.Services.ProgressService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IProgressService _progressService;
        public ProgressController(ILogger<EventsController> logger, IProgressService progressService)
        {
            _logger = logger;
            _progressService = progressService;
        }

        
        [HttpGet("{id:int}/CheckStatus")]
        public async Task<bool> CheckIfComplete(int id)
        {
            bool currentEventStatus = await _progressService.CheckEventStatus(id);
            return currentEventStatus;
        }

        [HttpGet("{id:int}/GetChecklistProgress")]
        public async Task<int> GetChecklistProgress(int id)
        {
            int checklistCount = await _progressService.GetChecklistCount(id);
            return checklistCount;
        }
    }
}
