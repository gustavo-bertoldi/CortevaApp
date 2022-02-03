using System;
namespace CortevaApp.Models
{
    public class Machine
    {
        public int id { get; set; }
        public string name { get; set; }
        public string operation { get; set; }
        public string fabricant { get; set; }
        public string modele { get; set; }
        public string productionline_name { get; set; }
        public string denomination_ordre { get; set; }
        public int ordre { get; set; }
        public int rejection { get; set; }
    }
}
