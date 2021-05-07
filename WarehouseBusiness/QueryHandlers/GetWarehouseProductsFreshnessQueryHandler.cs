using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Models;
using WarehouseBusiness.Queries;
using WarehouseBusiness.Services;
using WarehouseData;
using WarehouseData.Models;

namespace WarehouseBusiness.QueryHandlers
{
    public class GetWarehouseProductsFreshnessQueryHandler : IRequestHandler<GetWarehouseProductsFreshnessQuery, WarehouseProductFreshness>
    {
        private WarehouseContext _context;
        private IFreshnessStatusService _statusService;

        public GetWarehouseProductsFreshnessQueryHandler(WarehouseContext context, IFreshnessStatusService statusService)
        {
            _context = context;
            _statusService = statusService;
        }

        public async Task<WarehouseProductFreshness> Handle(GetWarehouseProductsFreshnessQuery request, CancellationToken cancellationToken)
        {
            var batches = await GetBatches();
            if (Empty(batches))
            {
                return null;
            }

            return batches.ToViewModel(_statusService);
        }

        private bool Empty(List<Batch> batches)
        {
            return batches is null || batches.Count == 0;
        }

        public async Task<List<Batch>> GetBatches()
        {
            return await _context.Batches.Include(x => x.Product)
                .ToListAsync();
        }
    }
}