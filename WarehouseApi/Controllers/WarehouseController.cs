using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Queries;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : Controller
    {
        private IMediator _mediator;

        public WarehouseController(IMediator mediator) => _mediator = mediator;

        [HttpGet("freshness")]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetWarehouseProductsFreshnessQuery(), cancellationToken);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}