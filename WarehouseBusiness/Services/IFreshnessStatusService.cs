using System;

namespace WarehouseBusiness.Services
{
    public interface IFreshnessStatusService
    {
        public string DetermineStatus(DateTime expirationDate);
    }
}