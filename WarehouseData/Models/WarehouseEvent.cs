using System;

namespace WarehouseData.Models
{
    public class WarehouseEvent
    {
        public int Id { get; set; }
        public DateTime RecorderAt { get; set; }
        public object Command { get; set; }
        public string Type { get; set; }
    }
}