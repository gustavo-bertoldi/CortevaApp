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
    public class EventsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EventsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{po}/{productionLine}")]
        public JsonResult getMachines(string po, string productionLine)
        {
            string queryPlannedEvents = @"select pe.duration as total_duration, pe.reason as type,
                                        pe.comment, pe.updated_at, pe.OLE, pe.productionline, pe.kind
                                        from dbo.ole_planned_events pe
                                        where pe.productionline = @productionLine
                                        and pe.OLE = @po";

            string queryChanginClients= @"select total_duration, type, comment, updated_at, OLE, productionline, kind
                                        from dbo.ole_unplanned_event_changing_clients
                                        where productionline = @productionLine
                                        and OLE = @po";

            string queryCIP = @"select total_duration, type, comment, updated_at, OLE, productionline, kind
                                from dbo.ole_unplanned_event_cips
                                where productionline = @productionLine
                                and OLE = @po";

            string queryUnplanned = @"select total_duration, type, comment, updated_at, OLE, productionline, kind
                                    from dbo.ole_unplanned_event_unplanned_downtimes
                                    where productionline = @productionLine
                                    and OLE = @po";

            string queryChangingFormats = @"select total_duration, type, comment, updated_at, OLE, productionline, kind
                                        from dbo.ole_unplanned_event_changing_formats
                                        where productionline = @productionLine
                                        and OLE = @po";

            DataTable plannedEvents = new DataTable();
            DataTable changingClients = new DataTable();
            DataTable CIP = new DataTable();
            DataTable unplanned = new DataTable();
            DataTable changingFormats = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryPlannedEvents, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    plannedEvents.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryChanginClients, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    changingClients.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryCIP, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    CIP.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryUnplanned, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    unplanned.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryChangingFormats, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    changingFormats.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            DataTable[] data = { plannedEvents, changingClients, CIP, unplanned, changingFormats };

            return new JsonResult(data);
        }
    }
}