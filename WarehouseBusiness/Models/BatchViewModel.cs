using System;

namespace WarehouseBusiness.Models
{
    public class BatchViewModel
    {
        public int Id { get; set;}
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

    }
}