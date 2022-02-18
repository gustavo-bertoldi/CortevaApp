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

namespace CortevaApp.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
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

        [HttpGet("countassignation/{username}/{po}/{productionline}")]
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


        [HttpPost("addassignation")]
        public JsonResult CreateAssignement(AssignementTeamPO Assignement)
        {
            string QueryNewAssignement = @"insert into dbo.ole_assignement_team_pos (username, productionline, po, shift, worksite)
                                       values (@Username, @ProductionLine, @PO, @Shift, @Worksite)";


            DataTable NewAssignement = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryNewAssignement, connection))
                {
                    command.Parameters.AddWithValue("@Username", Assignement.username);
                    command.Parameters.AddWithValue("@ProductionLine", Assignement.productionline);
                    command.Parameters.AddWithValue("@PO", Assignement.po);
                    command.Parameters.AddWithValue("@Shift", Assignement.shift);
                    command.Parameters.AddWithValue("@Worksite", Assignement.worksite);
                    reader = command.ExecuteReader();
                    NewAssignement.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(NewAssignement);
        }

        [HttpPost("PO")]
        public JsonResult CreatePO(PO po)
        {
            string QueryNewPO = @"insert into dbo.ole_pos (number, GMIDCode, productionline_name)
                                  values (@Number, @GMIDCode, @ProductionLine)";


            DataTable NewPO = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryNewPO, connection))
                {
                    command.Parameters.AddWithValue("@Number", po.number);
                    command.Parameters.AddWithValue("@GMIDCode", po.GMIDCode);
                    command.Parameters.AddWithValue("@ProductionLine", po.productionline_name);
                    reader = command.ExecuteReader();
                    NewPO.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(NewPO);
        }

        [HttpPost("stopPO/{PO}/{availability}/{performance}/{quality}/{OLE}/{quantityProduced}/{totalDuration}")]
        public JsonResult StopPO(string po, double availability, double performance, double quality, double OLE, int quantityProduced, int totalDuration)
        {
            string QueryStopPO = @"update dbo.ole_pos
                                   set state = 0,
                                   performance = @Performance,
                                   availability = @Availability,
                                   quality = @Quality,
                                   OLE = @OLE,
                                   qtyProduced = @QuantityProduced,
                                   workingDuration = @TotalDuration
                                   where number = @PO";


            DataTable StopPO = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryStopPO, connection))
                {
                    command.Parameters.AddWithValue("@Performance", performance);
                    command.Parameters.AddWithValue("@Availability", availability);
                    command.Parameters.AddWithValue("@Quality", quality);
                    command.Parameters.AddWithValue("@OLE", OLE);
                    command.Parameters.AddWithValue("@QuantityProduced", quantityProduced);
                    command.Parameters.AddWithValue("@TotalDuration", totalDuration);
                    command.Parameters.AddWithValue("@PO", po);
                    reader = command.ExecuteReader();
                    StopPO.Load(reader);
                    reader.Close();
                }

                
                 connection.Close();
            }

            return new JsonResult(StopPO);
        }
    }
}