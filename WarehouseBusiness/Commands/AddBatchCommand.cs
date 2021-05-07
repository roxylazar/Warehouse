using MediatR;
using System;

namespace WarehouseBusiness.Commands
{
    public class AddBatchCommand : IRequest<int>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}