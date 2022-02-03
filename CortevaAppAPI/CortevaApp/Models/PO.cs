using System;
namespace CortevaApp.Models
{
    public class PO
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string number { get; set; }
        public string GMIDCode { get; set; }
        public string productionline_name { get; set; }
        public int state { get; set; }
        public int totalOperatingTime { get; set; }
        public int totalNetOperatingTime { get; set; }
        public float Availability { get; set; }
        public float Performance { get; set; }
        public float Quality { get; set; }
        public float OLE { get; set; }
        public int qtyProduced { get; set; }
        public int workingDuration { get; set; }
    }
}
