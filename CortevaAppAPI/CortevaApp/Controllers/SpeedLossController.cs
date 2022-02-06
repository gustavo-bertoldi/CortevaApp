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
    public class SpeedLossController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public SpeedLossController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("speedLosses/{po}/{productionLine}")]
        public JsonResult GetSL(string po, string productionLine)
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

        [HttpGet("getSpeedLosses/{site}/{productionLine}/{startingDate}/{endingDate}")]
        public JsonResult GetSpeedLosses(string _, string productionLine, string startingDate, string endingDate)
        {
            string querySpeedLossesEvents = @"select sl.duration, sl.reason, sl.comment, pos.id, pos.qtyProduced, pos.workingDuration,
                                            prod.size, prod.idealRate
                                            from dbo.ole_speed_losses sl, dbo.ole_pos pos, dbo.ole_products prod
                                            where sl.productionline = @productionLine
                                            and sl.OLE = pos.number
                                            and prod.GMID = pos.GMIDCode
                                            and sl.created_at >= @startingDate
                                            and sl.created_at <= @endingDate";


            DataTable SpeedLossesEvents = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querySpeedLossesEvents, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startingDate", startingDate);
                    command.Parameters.AddWithValue("@endingDate", endingDate);
                    reader = command.ExecuteReader();
                    SpeedLossesEvents.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            IDictionary<string, DataTable> Result = new Dictionary<string, DataTable>()
            {
              { "SLEVENTS", SpeedLossesEvents }
            };

            return new JsonResult(Result);
        }
    }
}