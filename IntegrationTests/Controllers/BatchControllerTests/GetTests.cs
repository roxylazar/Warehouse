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

namespace IntegrationTests.Controllers.BatchControllerTests
{
    public class GetTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/batch/2";

        public GetTests(ITestOutputHelper testOutputHelper)
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
        public async Task WhenBatchFound_Returns_Ok()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch {
                        Id = 2,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product { Name = "nuts"},
                        Quantity = 100 }
                    });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task WhenBatchFound_Returns_Batch()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch> { new Batch {
                        Id = 2,
                        ExpirationDate = DateTime.Now.Date.AddDays(2),
                        Product = new Product { Name = "nuts"},
                        Quantity = 100 }
                    });

            var response = await _client.GetAsync(Uri);
            var result = await GetHttpResponseResult<BatchViewModel>(response);
           
            result.Id.Should().Be(2);
        }
    }
}