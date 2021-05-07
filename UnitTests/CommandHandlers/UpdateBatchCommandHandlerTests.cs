using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.CommandHandlers;
using WarehouseBusiness.Commands;
using WarehouseBusiness.Services;
using WarehouseData;
using WarehouseData.Models;
using Xunit;

namespace UnitTests.CommandHandlers
{
    public class UpdateBatchCommandHandlerTests : IDisposable
    {
        private Mock<WarehouseContext> _contextMock;
        private Mock<IFreshnessStatusService> _statusServiceMock;
        private UpdateBatchCommandHandler _handler;

        public UpdateBatchCommandHandlerTests()
        {
            _contextMock = new Mock<WarehouseContext>();
            _statusServiceMock = new Mock<IFreshnessStatusService>();
            _handler = new UpdateBatchCommandHandler(_contextMock.Object, _statusServiceMock.Object);
        }

        public void Dispose()
        {
            _handler = null;
        }

        [Fact]
        public async Task WhenBatchNotFound_Returns_Null()
        {
            var command = new UpdateBatchCommand { BatchId = 2 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch { Id = 1 } });

            var result = await _handler.Handle(command, new CancellationToken());

            result.Should().BeNull();
        }

        [Fact]
        public void WhenNotEnoughStockQuantity_ThrowsException()
        {
            var command = new UpdateBatchCommand { BatchId = 2, DeliveredQuantity = 10 };
            _contextMock.Setup(x => x.Batches)
               .ReturnsDbSet(new List<Batch> { new Batch { Id = 2, Quantity = 5 } });

            Func<Task> handle = async () => await _handler.Handle(command, new CancellationToken());

            handle.Should().Throw<Exception>().WithMessage("Not enough quantity in stock for batch 2");
        }

        [Fact]
        public async Task UpdatesBatchQuantity()
        {
            var command = new UpdateBatchCommand { BatchId = 2, DeliveredQuantity = 10 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch {
                        Id = 2,
                        Quantity = 15 ,
                        Product = new Product{ Name="pasta" },
                        ExpirationDate = DateTime.Now.AddDays(2)
                    } });

            var result = await _handler.Handle(command, new CancellationToken());

            result.Quantity.Should().Be(5);
        }

        [Fact]
        public async Task SavesData()
        {
            var command = new UpdateBatchCommand { BatchId = 2, DeliveredQuantity = 10 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch {
                        Id = 2,
                        Quantity = 15 ,
                        Product = new Product{ Name="pasta" },
                        ExpirationDate = DateTime.Now.AddDays(2)
                    } });


            await _handler.Handle(command, new CancellationToken());

            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}