using MediatR;
using System;

namespace WarehouseApi.Requests
{
    public class AddBatchRequest : IRequest<bool>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}