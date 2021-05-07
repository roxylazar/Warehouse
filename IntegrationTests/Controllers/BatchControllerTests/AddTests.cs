using FluentAssertions;
using IntegrationTest;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WarehouseApi;
using WarehouseApi.Requests;
using WarehouseData.Models;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Controllers.WarehouseControllerTests
{
    public class AddTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/batch";

        public AddTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task WhenRequestNotValid_Returns_BadRequest()
        {
            var request = new AddBatchRequest
            {
                Quantity = 0,
                ProductName = "test",
                ExpirationDate = DateTime.Now.AddDays(-1)
            };

            var result = await _client.PostAsJsonAsync(Uri, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task WhenRequestIsValid_Returns_Ok()
        {
            var request = new AddBatchRequest
            {
                Quantity = 12,
                ProductName = "test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };
            WarehouseContextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Name = "test" } });
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>());

            var result = await _client.PostAsJsonAsync(Uri, request);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenRequestIsValid_AddsBatch()
        {
            var request = new AddBatchRequest
            {
                Quantity = 12,
                ProductName = "test",
                ExpirationDate = DateTime.Now.AddDays(3)
            };
            WarehouseContextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Name = "test" } });
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>());

            var result = await _client.PostAsJsonAsync(Uri, request);

            WarehouseContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void WhenProductNotFound_ThrowsException()
        {
            var request = new AddBatchRequest
            {
                Quantity = 12,
                ProductName = "test",
                ExpirationDate = DateTime.Now.AddDays(2)
            };
            WarehouseContextMock.Setup(x => x.Products)
                .ReturnsDbSet(new List<Product> { new Product { Name = "sample" } });
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>());

            Func<Task> handle = async () => await _client.PostAsJsonAsync(Uri, request);

            handle.Should().Throw<Exception>().WithMessage("No product with name test was found.");
        }
    }
}