using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.CommandHandlers;
using WarehouseBusiness.Commands;
using WarehouseData;
using WarehouseData.Models;
using Xunit;

namespace UnitTests.CommandHandlers
{
    public class AddBatchCommandHandlerTests : IDisposable
    {
        private Mock<WarehouseContext> _contextMock;
        private AddBatchCommandHandler _handler;

        public AddBatchCommandHandlerTests()
        {
            _contextMock = new Mock<WarehouseContext>();
            _handler = new AddBatchCommandHandler(_contextMock.Object);
        }

        public void Dispose()
        {
            _handler = null;
        }

        [Fact]
        public void WhenProductNotFound_ThrowsException()
        {
            var command = new AddBatchCommand { ProductId = 1};
            _contextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Id = 2 } });

            Func<Task> handle = async () => await _handler.Handle(command, new CancellationToken());

            handle.Should().Throw<Exception>().WithMessage("No product with id 1 was found.");
        }

        [Fact]
        public async Task AddsBatch()
        {
            var command = new AddBatchCommand
            {
                ProductId = 2,
                Quantity = 10,
                ExpirationDate = DateTime.Now
            };

            _contextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Id = 2 } });
            _contextMock.Setup(x => x.Batches)
               .ReturnsDbSet(new List<Batch>());

            await _handler.Handle(command, new CancellationToken());

            _contextMock.Verify(x => x.Batches.Add(It.IsAny<Batch>()));
        }

        [Fact]
        public async Task SavesData()
        {
            var command = new AddBatchCommand
            {
                ProductId = 2,
                Quantity = 10,
                ExpirationDate = DateTime.Now
            };
            _contextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Id = 2 } });
            _contextMock.Setup(x => x.Batches)
               .ReturnsDbSet(new List<Batch>());

            await _handler.Handle(command, new CancellationToken());

            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}