using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBusiness.Models;
using WarehouseBusiness.Services;
using Xunit;

namespace UnitTests.Services
{
    public class StatusServiceTests : IDisposable
    {
        private IFreshnessStatusService _service;
        private Mock<IClockService> _clockServiceMock;

        public StatusServiceTests()
        {
            _clockServiceMock = new Mock<IClockService>();
            _service = new FreshnessStatusService(_clockServiceMock.Object);
        }

        public void Dispose()
        {
            _service = null;
        }

        [Fact]
        public void WhenExpirationDateIsToday_Returns_ExpiringToday()
        {
            var today = new DateTime(2021, 3, 12);
            _clockServiceMock.Setup(x => x.Now).Returns(today);

            var result = _service.DetermineStatus(today);

            result.Should().Be(Status.ExpiringToday.ToString());
        }

        [Fact]
        public void WhenExpirationDateIsLaterToday_Returns_ExpiringToday()
        {
            var today = new DateTime(2021, 3, 12);
            _clockServiceMock.Setup(x => x.Now).Returns(today);

            var result = _service.DetermineStatus(today.AddHours(7));

            result.Should().Be(Status.ExpiringToday.ToString());
        }

        [Fact]
        public void WhenExpirationDateIsYestarday_Returns_Expired()
        {
            var today = new DateTime(2021, 3, 12);
            _clockServiceMock.Setup(x => x.Now).Returns(today);

            var result = _service.DetermineStatus(today.AddDays(-1));

            result.Should().Be(Status.Expired.ToString());
        }

        [Fact]
        public void WhenExpirationDateIsTomorrow_Returns_Fresh()
        {
            var today = new DateTime(2021, 3, 12);
            _clockServiceMock.Setup(x => x.Now).Returns(today);

            var result = _service.DetermineStatus(today.AddDays(1));

            result.Should().Be(Status.Fresh.ToString());
        }
    }
}
