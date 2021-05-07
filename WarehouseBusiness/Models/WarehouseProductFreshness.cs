using System.Collections.Generic;

namespace WarehouseBusiness.Models
{
    public class WarehouseProductFreshness
    {
        public Dictionary<string, WarehouseProduct> Products { get; set; } = new Dictionary<string, WarehouseProduct>();
    }
}