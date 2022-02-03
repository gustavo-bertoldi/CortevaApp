using System;
namespace CortevaApp.Models
{
    public class Format
    {
        public int id { get; set; }
        public string format { get; set; }
        public string shape { get; set; }
        public string mat1 { get; set; }
        public string mat2 { get; set; }
        public string mat3 { get; set; }
        public int design_rate { get; set; }
        public string productionlineName { get; set; }
    }
}
