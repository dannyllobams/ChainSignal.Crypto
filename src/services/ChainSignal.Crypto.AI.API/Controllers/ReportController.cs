using ChainSignal.Crypto.AI.API.Application.Commands.GenerateDailyReport;
using ChainSignal.Crypto.AI.Domain.Model;
using ChainSignal.Crypto.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ChainSignal.Crypto.AI.API.Controllers
{
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IMediatorHandler _mediator;
        public ReportController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/report")]
        public async Task<IActionResult> GetReport()
        {
            var response = await _mediator.CallCommand<GenerateDailyReportCommand, MarketReport>(new GenerateDailyReportCommand());
            return Ok(response);
        }
    }
}
