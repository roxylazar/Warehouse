using MediatR;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Queries
{
    public class GetProductBatchInventoryQuery : IRequest<ProductBatchInventory>
    {
        public int ProductId { get; set; }
        public int BatchId { get; set; }
    }
}