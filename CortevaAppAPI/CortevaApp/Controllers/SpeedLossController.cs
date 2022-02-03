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
    [Route("api/[controller]")]
    [ApiController]
    public class SpeedLossController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public SpeedLossController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{po}/{productionLine}")]
        public JsonResult getMachines(string po, string productionLine)
        {
            string querySpeedLosses = @"select *
                                       from dbo.ole_speed_losses sl
                                       where sl.productionline = @productionLine
                                       and sl.OLE = @po";


            DataTable speedLosses = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querySpeedLosses, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    speedLosses.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(speedLosses);
        }
    }
}