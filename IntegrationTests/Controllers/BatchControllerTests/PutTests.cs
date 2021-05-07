using FluentAssertions;
using IntegrationTest;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WarehouseApi;
using WarehouseApi.Requests;
using WarehouseBusiness.Models;
using WarehouseData.Models;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Controllers.WarehouseControllerTests
{
    public class PutTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/batch?id=2";

        public PutTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task WhenRequestNotValid_Returns_BadRequest()
        {
            var request = new UpdateBatchRequest
            {
                DeliveredQuantity = 0,
                Description = "Delivered to FB"
            };

            var result = await _client.PutAsJsonAsync(Uri, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WhenRequestIsValid_Returns_Ok()
        {
            var request = new UpdateBatchRequest
            {
                DeliveredQuantity = 10,
                Description = "Delivered to FB"
            };

            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>{
                    new Batch {
                        Id = 2,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product { Name = "test"},
                        Quantity = 100 }
                    });

            var result = await _client.PutAsJsonAsync(Uri, request);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenRequestIsValid_UpdatesBatchQuantity()
        {
            var request = new UpdateBatchRequest
            {
                DeliveredQuantity = 10,
                Description = "Delivered to FB"
            };

            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>{
                    new Batch {
                        Id = 2,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product { Name = "test"},
                        Quantity = 100 }
                    });

            var response = await _client.PutAsJsonAsync(Uri, request);
            var result = await GetHttpResponseResult<BatchViewModel>(response);

            result.Quantity.Should().Be(90);
        }

        [Fact]
        public async Task WhenBatchNotFound_Returns_NotFound()
        {
            var request = new UpdateBatchRequest
            {
                DeliveredQuantity = 10,
                Description = "Delivered to FB"
            };

            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch { Id = 3 } });

            var result = await _client.PutAsJsonAsync(Uri, request);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
