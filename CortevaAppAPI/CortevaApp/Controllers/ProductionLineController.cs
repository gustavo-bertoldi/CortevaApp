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
    public class ProductionLineController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ProductionLineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("productionlineID/{productionLine}")]
        public JsonResult GetProductionLineID(string productionLine)
        {
            string QueryProductionLineID= @"select id
                                           from dbo.ole_productionline
                                           where productionline_name = @productionLine";

            DataTable ProductionLineID = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryProductionLineID, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    reader = command.ExecuteReader();
                    ProductionLineID.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(ProductionLineID);
        }
    }
}