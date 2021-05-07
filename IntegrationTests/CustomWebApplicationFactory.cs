using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WarehouseBusiness.Services;
using WarehouseData;
using Xunit.Abstractions;

namespace IntegrationTest
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
        {
            ClientOptions.AllowAutoRedirect = false;
        }

        public Mock<WarehouseContext> WarehouseContextMock { get; } = new Mock<WarehouseContext>();

        public Mock<IClockService> ClockServiceMock { get; } = new Mock<IClockService>();

        public void VerifyAllMocks() => Mock.VerifyAll(WarehouseContextMock, ClockServiceMock);

        protected override void ConfigureClient(HttpClient client)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
            }

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder
                .UseEnvironment("Test")
                .ConfigureServices(ConfigureServices);

        protected virtual void ConfigureServices(IServiceCollection services) =>
            services
                .AddSingleton(WarehouseContextMock.Object)
                .AddSingleton(ClockServiceMock.Object);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected async Task<T> GetHttpResponseResult<T>(HttpResponseMessage message)
        {
            var responseBody = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}