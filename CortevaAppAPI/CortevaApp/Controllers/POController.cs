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
    public class POController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public POController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("po/{po}")]
        public JsonResult IsPOPossible(string po)
        {
            string QueryPon = @"select count(*) as result
                                from dbo.ole_pos po
                                where po.number = @po";


            DataTable Pon = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryPon, connection))
                {
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    Pon.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(Pon.Select()[0][0]);
        }

        [HttpGet("pos/{shift}/{site}")]
        public JsonResult getMachines(string shift, string site)
        {
            string queryPos = @"select *
                                from dbo.ole_assignement_team_pos atp, dbo.ole_pos pos, dbo.worksite w
                                where atp.worksite = w.id
                                and pos.number = atp.po
                                and w.name = @site
                                and atp.shift = @shift";


            DataTable pos = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryPos, connection))
                {
                    command.Parameters.AddWithValue("@site", site);
                    command.Parameters.AddWithValue("@shift", shift);
                    reader = command.ExecuteReader();
                    pos.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(pos);
        }

        [HttpGet("assignation/{username}/{po}/{productionline}")]
        public JsonResult isAssignantionPossible(string username, string po, int productionLine)
        {
            string QueryAssignation = @"select count(*) as result
                                       from dbo.ole_assignement_team_pos atp
                                       where atp.username = @username
                                       and atp.po = @po
                                       and atp.productionline = @productionLine";


            DataTable Assignation = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryAssignation, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@po", po);
                    command.Parameters.AddWithValue("@productionline", productionLine);
                    reader = command.ExecuteReader();
                    Assignation.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(Assignation.Select()[0][0]);
        }
    }
}