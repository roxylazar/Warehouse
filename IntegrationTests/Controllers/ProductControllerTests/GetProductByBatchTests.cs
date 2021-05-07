using FluentAssertions;
using IntegrationTest;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WarehouseApi;
using WarehouseBusiness.Models;
using WarehouseData.Models;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Controllers.ProductControllerTests
{
    public class GetProductByBatchTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/product/1/batch/1";

        public GetProductByBatchTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task WhenBatchNotFound_Returns_NotFound()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch { Id = 3 } });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenBatchWithProductNotFound_Returns_NotFound()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch { Id = 1, Product = new Product { Id = 4 } } });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenBatchFound_Returns_Ok()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch {
                        Id = 1,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product {Id = 1, Name = "cashew"},
                        Quantity = 75 }
                    });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenBatchWithProductFound_Returns_ProductBatchInventory()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch {
                        Id = 1,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product {Id = 1, Name = "cashew"},
                        Quantity = 133 }
                    });

            var response = await _client.GetAsync(Uri);
            var result = await GetHttpResponseResult<ProductBatchInventory>(response);

            result.ProductName.Should().Be("cashew");
        }
    }
}