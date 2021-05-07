using MediatR;
using System;

namespace WarehouseBusiness.Commands
{
    public class AddBatchCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}