using Microsoft.AspNetCore.Mvc;
using NowEvent.Services.ProgressService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;
        public ProgressController(IProgressService progressService)
        {
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
