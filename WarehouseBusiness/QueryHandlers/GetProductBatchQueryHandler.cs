using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Models;
using WarehouseBusiness.Queries;
using WarehouseData;
using WarehouseData.Models;

namespace WarehouseBusiness.QueryHandlers
{
    public class GetProductBatchQueryHandler : IRequestHandler<GetProductBatchInventoryQuery, ProductBatchInventory>
    {
        private WarehouseContext _context;

        public GetProductBatchQueryHandler(WarehouseContext context)
        {
            _context = context;
        }

        public Task<ProductBatchInventory> Handle(GetProductBatchInventoryQuery request, CancellationToken cancellationToken)
        {
            var batch = GetBatch(request);
            if (IsNull(batch))
            {
                return Task.FromResult<ProductBatchInventory>(null);
            }

            return Task.FromResult(batch.ToViewModel());
        }

        private bool IsNull(Batch batch)
        {
            return batch is null;
        }

        private Batch GetBatch(GetProductBatchInventoryQuery request)
        {
            return _context.Batches
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == request.BatchId && x.Product.Id == request.ProductId);
        }
    }
}