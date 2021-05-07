using System.Collections.Generic;
using System.Linq;
using WarehouseBusiness.Services;
using WarehouseData.Models;

namespace WarehouseBusiness.Models
{
    public static class ModelMapper
    {
        public static BatchViewModel ToViewModel(this Batch batch, IFreshnessStatusService statusService)
        {
            return new BatchViewModel
            {
                Id = batch.Id,
                ProductName = batch.Product.Name,
                ExpirationDate = batch.ExpirationDate,
                Quantity = batch.Quantity,
                Status = statusService.DetermineStatus(batch.ExpirationDate)
            };
        }

        public static ProductBatchInventory ToViewModel(this Batch batch)
        {
            return new ProductBatchInventory
            {
                BatchId = batch.Id,
                ProductName = batch.Product.Name,
                ProductId = batch.Product.Id,
                Quantity = batch.Quantity
            };
        }

        public static ProductInventory ToViewModel(this List<Batch> batches)
        {
            return batches.Aggregate(new ProductInventory(),
                (product, batch) =>
                {
                    product.ProductId = batch.Product.Id;
                    product.ProductName = batch.Product.Name;
                    product.Batches.Add(new BatchInventory { BatchId = batch.Id, Quantity = batch.Quantity });
                    return product;
                });
        }

        public static WarehouseProductFreshness ToViewModel(this List<Batch> batches, IFreshnessStatusService statusService)
        {
            return batches.Aggregate(new WarehouseProductFreshness(),
                (warehouse, batch) =>
                {
                    if (!warehouse.Products.TryGetValue(
                        batch.Product.Name, out var product))
                    {
                        product = new WarehouseProduct()
                        {
                            ProductId = batch.Product.Id,
                            ProductName = batch.Product.Name
                        };
                        warehouse.Products[batch.Product.Name] = product;
                    }

                    product.Batches.Add(new WarehouseBatch
                    {
                        BatchId = batch.Id,
                        Quantity = batch.Quantity,
                        Freshness = statusService.DetermineStatus(batch.ExpirationDate)
                    });

                    return warehouse;
                });
        }
    }
}