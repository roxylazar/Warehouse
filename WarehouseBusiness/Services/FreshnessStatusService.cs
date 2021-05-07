using System;
using WarehouseBusiness.Models;

namespace WarehouseBusiness.Services
{
    public class FreshnessStatusService : IFreshnessStatusService
    {
        private IClockService _clockService;

        public FreshnessStatusService(IClockService clockService) => _clockService = clockService;

        public string DetermineStatus(DateTime expirationDate)
        {
            if (expirationDate.Date == _clockService.Now.Date)
                return Status.ExpiringToday.ToString();
            if (expirationDate.Date < _clockService.Now.Date)
                return Status.Expired.ToString();

            return Status.Fresh.ToString();
        }
    }
}