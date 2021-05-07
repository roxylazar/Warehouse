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
using WarehouseBusiness.Models;
using WarehouseData.Models;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Controllers.ProductController
{
    public class GetWarehouseProductsTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/product/1/warehouse";

        public GetWarehouseProductsTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task WhenNoBatchWithProductFound_Returns_NotFound()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch { Id = 5, Product  = new Product { Id = 2 } },
                    new Batch { Id = 7, Product  = new Product { Id = 9 } }
                 });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenBatchWithProductFound_Returns_Ok()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch { Id = 5, Product  = new Product { Id = 1 } },
                    new Batch { Id = 7, Product  = new Product { Id = 1 } }
                 });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task WhenBatchWithProductFound_Returns_ProductInventory()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> {
                    new Batch { Id = 5, Product  = new Product { Id = 1 , Name = "pasta"}, Quantity = 25},
                    new Batch { Id = 7, Product  = new Product { Id = 1 , Name = "pasta"}, Quantity = 14}
                    });

            var response = await _client.GetAsync(Uri);
            var result = await GetHttpResponseResult<ProductInventory>(response);

            result.ProductName.Should().Be("pasta");
            result.Batches.Count.Should().Be(2);
        }
    }
}
