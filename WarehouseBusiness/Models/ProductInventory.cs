using System.Collections.Generic;

namespace WarehouseBusiness.Models
{
    public class ProductInventory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<BatchInventory> Batches { get; set; } = new List<BatchInventory>();
    }
}