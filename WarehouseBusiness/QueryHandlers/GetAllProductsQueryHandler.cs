using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Queries;
using WarehouseData;
using WarehouseData.Models;

namespace WarehouseBusiness.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private WarehouseContext _context;

        public GetAllProductsQueryHandler(WarehouseContext context) => _context = context;

        public Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _context.Products.ToListAsync();
        }
    }
}