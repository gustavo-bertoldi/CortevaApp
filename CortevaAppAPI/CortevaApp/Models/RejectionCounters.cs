using System;
namespace CortevaApp.Models
{
    public class RejectionCounters
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string po { get; set; }
        public int fillerCounter { get; set; }
        public int caperCounter { get; set; }
        public int labelerCounter { get; set; }
        public int weightBoxCounter { get; set; }
        public int qualityControlCounter { get; set; }
        public int fillerRejection { get; set; }
        public int caperRejection { get; set; }
        public int labelerRejection { get; set; }
        public int weightBoxRejection { get; set; }
        public int qualityControlRejection { get; set; }
    }
}
