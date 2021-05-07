using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Queries;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}/batch/{batchId}")]
        public async Task<IActionResult> GetProductByBatchAsync(int id, int batchId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductBatchInventoryQuery { ProductId = id, BatchId = batchId }, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}/warehouse")]
        public async Task<IActionResult> GetWarehouseProductsAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductInventoryQuery { ProductId = id }, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}