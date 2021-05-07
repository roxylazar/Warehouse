using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseBusiness.Services
{
    public interface IClockService
    {
        DateTime Now { get; }
    }
}
