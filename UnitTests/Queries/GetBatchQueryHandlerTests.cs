using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBusiness.Queries;
using WarehouseBusiness.QueryHandlers;
using WarehouseBusiness.Services;
using WarehouseData;
using WarehouseData.Models;
using Xunit;

namespace UnitTests.Queries
{
    public class GetBatchQueryHandlerTests : IDisposable
    {
        private GetBatchQueryHandler _handler;
        private Mock<WarehouseContext> _contextMock;
        private Mock<IFreshnessStatusService> _statusServiceMock;

        public GetBatchQueryHandlerTests()
        {
            _contextMock = new Mock<WarehouseContext>();
            _statusServiceMock = new Mock<IFreshnessStatusService>();
            _handler = new GetBatchQueryHandler(_contextMock.Object, _statusServiceMock.Object);
        }

        public void Dispose()
        {
            _handler = null;
        }

        [Fact]
        public async Task WhenBatchNotFound_Returns_Null()
        {
            var query = new GetBatchQuery { BatchId = 10 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch { Id = 13 } });

            var result = await _handler.Handle(query, new CancellationToken());

            result.Should().BeNull();
        }

        [Fact]
        public async Task WhenBatchFound_Returns_Data()
        {
            var query = new GetBatchQuery { BatchId = 10 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch {
                        Id = 10,
                        Product = new Product { Name = "yogurt"},
                    }
                });

            var result = await _handler.Handle(query, new CancellationToken());

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenBatchFound_Returns_CorrectBatch()
        {
            var query = new GetBatchQuery { BatchId = 10 };
            _contextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch {
                        Id = 10,
                        Product = new Product { Name = "yogurt"},
                    }
                });

            var result = await _handler.Handle(query, new CancellationToken());

            result.Id.Should().Be(10);
        }
    }
}