using System.Collections.Generic;

namespace WarehouseBusiness.Models
{
    public class WarehouseProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public List<WarehouseBatch> Batches { get;set;} = new List<WarehouseBatch>();
    }
}