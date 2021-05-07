using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WarehouseApi.Requests;
using WarehouseBusiness.Queries;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private IMediator _mediator;

        public BatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddBatchRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.ToCommand(), cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateBatchRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.ToCommand(id), cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBatchQuery { BatchId = id }, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}