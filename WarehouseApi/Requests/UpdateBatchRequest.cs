using MediatR;
using WarehouseData.Models;

namespace WarehouseApi.Requests
{
    public class UpdateBatchRequest : IRequest<Batch>
    {
        public int? DeliveredQuantity { get; set; }
        public int? AddedQuantity { get; set; }
        public string Description { get; set; }
    }
}