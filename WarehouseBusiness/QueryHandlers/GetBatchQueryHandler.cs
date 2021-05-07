using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Models;
using WarehouseBusiness.Queries;
using WarehouseBusiness.Services;
using WarehouseData;

namespace WarehouseBusiness.QueryHandlers
{
    public class GetBatchQueryHandler : IRequestHandler<GetBatchQuery, BatchViewModel>
    {
        private WarehouseContext _context;
        private IFreshnessStatusService _statusService;

        public GetBatchQueryHandler(WarehouseContext context, IFreshnessStatusService statusService)
        {
            _context = context;
            _statusService = statusService;
        }

        public Task<BatchViewModel> Handle(GetBatchQuery request, CancellationToken cancellationToken)
        {
            var batch = GetBatch(request.BatchId);
            if (IsNull(batch))
            {
                return Task.FromResult<BatchViewModel>(null);
            }

            return Task.FromResult(batch.ToViewModel(_statusService));
        }

        private bool IsNull(WarehouseData.Models.Batch batch)
        {
            return batch is null;
        }

        private WarehouseData.Models.Batch GetBatch(int id)
        {
            return _context.Batches.Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}