using MediatR;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Commands
{
    public class UpdateBatchCommand : IRequest<BatchViewModel>
    {
        public int DeliveredQuantity { get; set; }
        public int AddedQuantity { get; set; }
        public string Description { get; set; }
        public int BatchId { get; set; }
    }
}