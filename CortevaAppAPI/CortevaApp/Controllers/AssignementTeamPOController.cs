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
    [Route("api/pos")]
    [ApiController]
    public class AssignementTeamPOController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AssignementTeamPOController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{shift}/{site}")]
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
    }
}