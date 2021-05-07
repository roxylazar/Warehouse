using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Commands;
using WarehouseData;
using WarehouseData.Models;

namespace WarehouseBusiness.CommandHandlers
{
    public class AddBatchCommandHandler : IRequestHandler<AddBatchCommand, int>
    {
        private WarehouseContext _context;

        public AddBatchCommandHandler(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddBatchCommand command, CancellationToken cancellationToken)
        {
            var product = FindProduct(command.ProductId);

            ThrowWhenProductIsNull(command, product);

            return await AddBatch(command, product, cancellationToken);
        }

        private static void ThrowWhenProductIsNull(AddBatchCommand command, Product product)
        {
            if (product is null)
            {
                throw new Exception($"No product with id {command.ProductId} was found.");
            }
        }

        private async Task<int> AddBatch(AddBatchCommand command, Product product, CancellationToken cancellationToken)
        {
            var batch = new Batch
            {
                Product = product,
                ExpirationDate = command.ExpirationDate,
                Quantity = command.Quantity
            };

            _context.Batches.Add(batch);

            await _context.SaveChangesAsync(cancellationToken);

            return batch.Id;
        }

        private Product FindProduct(int productId)
        {
            return _context.Products.FirstOrDefault(x => x.Id == productId);
        }
    }
}