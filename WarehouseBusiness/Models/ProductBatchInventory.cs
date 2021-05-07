namespace WarehouseBusiness.Models
{
    public class ProductBatchInventory
    {
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}