using System;
namespace CortevaApp.Models
{
    public class PlannedEvent
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string OLE { get; set; }
        public string productionline { get; set; }
        public string reason { get; set; }
        public int duration { get; set; }
        public string comment { get; set; }
        public int kind { get; set; }
    }
}
