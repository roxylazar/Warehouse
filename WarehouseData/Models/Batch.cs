using System;

namespace WarehouseData.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}