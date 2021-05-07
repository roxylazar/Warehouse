using System;

namespace WarehouseBusiness.Services
{
    public class ClockService : IClockService
    {
        public DateTime Now => DateTime.Now;
    }
}