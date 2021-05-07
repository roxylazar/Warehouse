using WarehouseBusiness.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseData;
using WarehouseData.Models;
using Microsoft.EntityFrameworkCore;
using WarehouseBusiness.Models;
using WarehouseBusiness.Services;

namespace WarehouseBusiness.CommandHandlers
{
    public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, BatchViewModel>
    {
        private WarehouseContext _context;
        private IFreshnessStatusService _statusService;

        public UpdateBatchCommandHandler(WarehouseContext context, IFreshnessStatusService statusService)
        {
            _context = context;
            _statusService = statusService;
        }

        public async Task<BatchViewModel> Handle(UpdateBatchCommand command, CancellationToken cancellationToken)
        {
            var batch = FindBatch(command.BatchId);

            if (IsNull(batch))
            {
                return await Task.FromResult<BatchViewModel>(null);
            }

            ThrowWhenNotEnoughQuantity(batch, command);

            return await UpdateBatchQuantity(command, batch);

        }

        private async Task<BatchViewModel> UpdateBatchQuantity(UpdateBatchCommand command, Batch batch)
        {
            batch.Quantity += command.AddedQuantity;
            batch.Quantity -= command.DeliveredQuantity;

            _context.Batches.Update(batch);

            await _context.SaveChangesAsync();

            return batch.ToViewModel(_statusService);
        }

        private static void ThrowWhenNotEnoughQuantity(Batch batch, UpdateBatchCommand command)
        {
            int inStockQuantity = batch.Quantity + command.AddedQuantity;
            if (inStockQuantity < command.DeliveredQuantity)
            {
                throw new Exception($"Not enough quantity in stock for batch {batch.Id}");
            }
        }

        private bool IsNull(Batch batch)
        {
            return batch is null;
        }

        private Batch FindBatch(int id)
        {
            return _context.Batches.Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}