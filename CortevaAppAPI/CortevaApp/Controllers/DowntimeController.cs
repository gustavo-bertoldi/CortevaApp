using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CortevaApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class DowntimeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public DowntimeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getMachines/{productionName}/{downtimeType}/unplannedDowntime")]
        public JsonResult GetUnplannedDowntime2(string _, string __)
        {
            string queryUnplannedDowntime2 = @"select *
                                              from dbo.ole_machines 
                                              where name != 'filler'";

            DataTable UnplannedDowntime2 = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryUnplannedDowntime2, connection))
                {
                    reader = command.ExecuteReader();
                    UnplannedDowntime2.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(UnplannedDowntime2);
        }

        [HttpGet("summary/{productionName}/{downtimeType}")]
        public JsonResult GetSummary(string _, string downtimeType)
        {
            string queryDowntimeReason = @"select *
                                          from dbo.ole_downtimeReason 
                                          where downtimeType = @downtimeType";

            DataTable downtimeReason = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryDowntimeReason, connection))
                {
                    command.Parameters.AddWithValue("@downtimeType", downtimeType);
                    reader = command.ExecuteReader();
                    downtimeReason.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(downtimeReason);
        }
    }
}