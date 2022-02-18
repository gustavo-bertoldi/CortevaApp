using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CortevaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [Authorize]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            string UserAuthQuery = @"select count(*) as auth
                                   from dbo.users u
                                   where u.login = @username and u.password = @password";


            DataTable UserAuth = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UserAuthQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", user.login);
                    command.Parameters.AddWithValue("@password", user.password);
                    reader = command.ExecuteReader();
                    UserAuth.Load(reader);
                    reader.Close();
                }

                connection.Close();
            }

            int result = int.Parse(UserAuth.Rows[0]["auth"].ToString());

            if (result > 0)
            {
                var tokenString = GenerateJWT(user);
                return Ok(new { token = tokenString });
            } else
            {
                return Unauthorized();
            }
        }

        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var ExpDate = DateTime.UtcNow.AddMinutes(720);
            var claims = new[]
            {
                new Claim("login", user.login)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: ExpDate,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 



    }
}