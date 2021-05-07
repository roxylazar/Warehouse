using MediatR;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Queries
{
    public class GetWarehouseProductsFreshnessQuery : IRequest<WarehouseProductFreshness>
    {
    }
}