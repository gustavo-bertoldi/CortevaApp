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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("user/{username}")]
        public JsonResult GetUserInfo(string username)
        {
            string userInfoQuery = @"select *
                                   from dbo.users u, dbo.worksite w
                                   where u.worksite_name = w.name and u.login = @username";

            string crewLeadersQuery = @"select *
                                      from dbo.users u, dbo.worksite w
                                      where u.worksite_name = w.name and u.status = 1 and u.worksite_name = (Select worksite_name
                                                                                                              from dbo.users u2, dbo.worksite w2
                                                                                                                where u2.worksite_name = w2.name and u2.login = @username )";

            string shiftsQuery = @"select *
                                 from dbo.users u, dbo.teamInfo ti, dbo.worksite w
                                 where u.worksite_name = ti.worksite_name
                                 and u.worksite_name = w.name
                                 and u.login = @username";

            string productionLineQuery = @"select pl.productionline_name, w.id
                                        from dbo.users u, dbo.ole_productionline pl, dbo.worksite w
                                        where u.worksite_name = w.name
                                        and pl.worksite_name = w.name
                                        and u.login = @username";


            DataTable userInfo = new DataTable();
            DataTable crewLeaders = new DataTable();
            DataTable shifts = new DataTable();
            DataTable productionLine = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(userInfoQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    reader = command.ExecuteReader();
                    userInfo.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(crewLeadersQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    reader = command.ExecuteReader();
                    crewLeaders.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(shiftsQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    reader = command.ExecuteReader();
                    shifts.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(productionLineQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    reader = command.ExecuteReader();
                    productionLine.Load(reader);
                    reader.Close();
                }

                connection.Close();
            }

            DataTable[] data = { userInfo, crewLeaders, shifts, productionLine };

            return new JsonResult(data);
        }

      

    }
}