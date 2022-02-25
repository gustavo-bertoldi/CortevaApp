using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CortevaApp.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("netOP/{GMID}")]
        public JsonResult GetNetOP(int GMID)
        {
            string QueryNetOP = @"select top(1) *
                                from dbo.ole_products
                                where GMID = @GMID";


            DataTable NetOP = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryNetOP, connection))
                {
                    command.Parameters.AddWithValue("@GMID", GMID);
                    reader = command.ExecuteReader();
                    NetOP.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(NetOP);
        }
    }
}