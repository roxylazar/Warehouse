using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Models;
using WarehouseBusiness.Queries;
using WarehouseData;
using WarehouseData.Models;

namespace WarehouseBusiness.QueryHandlers
{
    public class GetProductInventoryQueryHandler : IRequestHandler<GetProductInventoryQuery, ProductInventory>
    {
        private WarehouseContext _context;

        public GetProductInventoryQueryHandler(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<ProductInventory> Handle(GetProductInventoryQuery request, CancellationToken cancellationToken)
        {
            var batches = await GetBatchesForProduct(request.ProductId);
            if (Empty(batches))
            {
                return null;
            }

            return batches.ToViewModel();
        }

        private bool Empty(List<Batch> batches)
        {
            return batches is null || batches.Count == 0;
        }

        public async Task<List<Batch>> GetBatchesForProduct(int productId)
        {
            return await _context.Batches.Include(x => x.Product)
                .Where(x => x.Product.Id == productId)
                .ToListAsync();
        }
    }
}