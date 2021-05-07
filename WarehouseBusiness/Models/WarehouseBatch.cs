namespace WarehouseBusiness.Models
{
    public class WarehouseBatch 
    {
        public int BatchId { get; set; }
        public int Quantity { get; set; }
        public string Freshness { get; set; }
    }
}