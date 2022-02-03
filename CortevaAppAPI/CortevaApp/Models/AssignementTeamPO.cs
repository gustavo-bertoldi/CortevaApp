using System;
namespace CortevaApp.Models
{
    public class AssignementTeamPO
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string username { get; set; }
        public string productionline { get; set; }
        public string po { get; set; }
        public string shift { get; set; }
        public int worksite { get; set; }
    }
}
