using System;
namespace CortevaApp.Models
{
    public class SpeedLoss
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string OLE { get; set; }
        public string productionline { get; set; }
        public int duration { get; set; }
        public string reason { get; set; }
        public string comment { get; set; }
    }
}
