using MediatR;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Queries
{
    public class GetProductInventoryQuery : IRequest<ProductInventory>
    {
        public int ProductId { get; set; }
    }
}