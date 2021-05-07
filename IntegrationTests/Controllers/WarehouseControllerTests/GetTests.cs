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

namespace IntegrationTests.Controllers.WarehouseControllerTests
{
    public class GetTests : CustomWebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private const string Uri = "/api/warehouse/freshness";
        private DateTime _today;

        public GetTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _client = CreateClient();
            _today = new DateTime(2021, 4, 21);
            ClockServiceMock.Setup(x => x.Now).Returns(_today);
        }

        [Fact]
        public async Task WhenNoData_Returns_NotFound()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                .ReturnsDbSet(new List<Batch>());

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                 .ReturnsDbSet(new List<Batch> { new Batch {
                        Id = 1,
                        ExpirationDate = _today.AddDays(2),
                        Product = new Product {Id = 1, Name = "cashew"},
                        Quantity = 10 }
                     });

            var result = await _client.GetAsync(Uri);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Returns_ExpectedFreshness()
        {
            WarehouseContextMock.Setup(x => x.Batches)
                 .ReturnsDbSet(new List<Batch> {
                     new Batch {
                        Id = 1,
                        ExpirationDate = _today.AddDays(2),
                        Product = new Product {Id = 1, Name = "cashew"},
                        Quantity = 10
                     },
                     new Batch
                      {
                          Id = 2,
                          ExpirationDate = _today.AddDays(-1),
                          Product = new Product { Id = 1, Name = "cashew" },
                          Quantity = 5
                      },
                     new Batch
                      {
                          Id = 3,
                          ExpirationDate = _today,
                          Product = new Product { Id = 2, Name = "polenta" },
                          Quantity = 15
                      },
                     });

            var response = await _client.GetAsync(Uri);
            var result = await GetHttpResponseResult<WarehouseProductFreshness>(response);

            result.Products["cashew"].Batches[0].Freshness.Should().Be(Status.Fresh.ToString());
            result.Products["cashew"].Batches[1].Freshness.Should().Be(Status.Expired.ToString());
            result.Products["polenta"].Batches[0].Freshness.Should().Be(Status.ExpiringToday.ToString());
        }
    }
}