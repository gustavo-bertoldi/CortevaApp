using System;
namespace CortevaApp.Models
{
    public class UnplannedEventChangingClient
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string OLE { get; set; }
        public string productionline { get; set; }
        public int predicted_duration { get; set; }
        public int total_duration { get; set; }
        public string lot_number { get; set; }
        public string comment { get; set; }
        public string type { get; set; }
        public int kind { get; set; }
    }
}
