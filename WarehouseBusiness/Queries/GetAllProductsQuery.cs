using MediatR;
using System.Collections.Generic;
using WarehouseData.Models;

namespace WarehouseBusiness.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}