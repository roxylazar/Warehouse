using MediatR;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Queries
{
    public class GetBatchQuery : IRequest<BatchViewModel>
    {
        public int BatchId { get; set; }
    }
}