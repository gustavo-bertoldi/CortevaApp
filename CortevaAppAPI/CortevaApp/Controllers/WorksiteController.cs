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
    public class WorksiteController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public WorksiteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("sites")]
        public JsonResult getSites()
        {
            string querySites = @"select * from dbo.worksite";
            string queryProductionLines  = @"select pl.id, pl.productionline_name, w.id, w.name
                                            from dbo.ole_productionline pl, dbo.worksite w
                                            where w.name = pl.worksite_name";

            DataTable sites = new DataTable();
            DataTable productionLines = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querySites, connection))
                {
                    reader = command.ExecuteReader();
                    sites.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryProductionLines, connection))
                {
                    reader = command.ExecuteReader();
                    productionLines.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            DataTable[] data = { sites, productionLines }; 

            return new JsonResult(data);
        }

        [HttpGet("worksiteid/{worksite}")]
        public JsonResult GetWorksiteID(string worksite)
        {
            string QueryWorksiteID = @"select id
                                       from dbo.worksite
                                       where name = @worksite";

            DataTable WorksiteID = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryWorksiteID, connection))
                {
                    command.Parameters.AddWithValue("@worksite", worksite);
                    reader = command.ExecuteReader();
                    WorksiteID.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(WorksiteID);
        }
    }
}