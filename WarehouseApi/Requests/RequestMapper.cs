using WarehouseBusiness.Commands;

namespace WarehouseApi.Requests
{
    public static class RequestMapper
    {
        public static AddBatchCommand ToCommand(this AddBatchRequest request)
        {
            return new AddBatchCommand
            {
                ProductName = request.ProductName,
                ExpirationDate = request.ExpirationDate,
                Quantity = request.Quantity
            };
        }

        public static UpdateBatchCommand ToCommand(this UpdateBatchRequest request, int id)
        {
            return new UpdateBatchCommand
            {
                AddedQuantity = request.AddedQuantity.HasValue ? request.AddedQuantity.Value : 0,
                DeliveredQuantity = request.DeliveredQuantity.HasValue ? request.DeliveredQuantity.Value : 0,
                Description = request.Description,
                BatchId = id
            };
        }
    }
}