using MediatR;
using System;

namespace WarehouseApi.Requests
{
    public class AddBatchRequest : IRequest<bool>
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}