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
    public class EventsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EventsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("events/{po}/{productionLine}")]
        public JsonResult GetEvents(string po, string productionLine)
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

        [HttpGet("UnplannedDowntimeEvents/{productionLine}/{startYear}/{endYear}")]
        public JsonResult GetUnplannedDowntimeEvents(string productionLine, int startYear, int endYear)
        {
            string queryCIP = @"select *
                                from dbo.ole_unplanned_event_cips cip
                                where cip.productionline = @productionLine
                                and year(cip.created_at) >= @startYear
                                and year(cip.created_at) <= @endYear";

            string queryCOV = @"select *
                                from dbo.ole_unplanned_event_changing_clients cov
                                where cov.productionline = @productionLine
                                and year(cov.created_at) >= @startYear
                                and year(cov.created_at) <= @endYear";

            string queryBNC = @"select *
                                from dbo.ole_unplanned_event_changing_formats bnc
                                where bnc.productionline = @productionLine
                                and year(bnc.created_at) >= @startYear
                                and year(bnc.created_at) <= @endYear";

            string queryMachineShutdowns = @"select *
                                            from dbo.ole_unplanned_event_unplanned_downtimes
                                            where productionline = @productionLine
                                            and implicated_machine != 'other'
                                            and year(created_at) >= @startYear
                                            and year(created_at) <= @endYear";

            string queryExternalShutdowns = @"select *
                                            from dbo.ole_unplanned_event_unplanned_downtimes
                                            where productionline = @productionLine
                                            and implicated_machine = 'other'
                                            and year(created_at) >= @startYear
                                            and year(created_at) <= @endYear";

            string querySeqCips = @"select *
                                    from dbo.ole_unplanned_event_cips cip, dbo.ole_pos pos, dbo.ole_products prod
                                    where cip.productionline = @productionLine
                                    and year(cip.created_at) >= @startYear
                                    and year(cip.created_at) <= @endYear
                                    and pos.number = cip.OLE
                                    and prod.GMID = pos.GMIDCode";


            string querySeqCovs = @"select *
                                    from dbo.ole_unplanned_event_changing_clients cov, dbo.ole_pos pos, dbo.ole_products prod
                                    where cov.productionline = @productionLine
                                    and year(cov.created_at) >= @startYear
                                    and year(cov.created_at) <= @endYear
                                    and pos.number = cov.OLE
                                    and prod.GMID = pos.GMIDCode";

            DataTable CIP = new DataTable();
            DataTable COV = new DataTable();
            DataTable BNC = new DataTable();
            DataTable machineShutdowns = new DataTable();
            DataTable externalShutdowns = new DataTable();
            DataTable seqCips = new DataTable();
            DataTable seqCovs = new DataTable();



            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryCIP, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    CIP.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryCOV, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    COV.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryBNC, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    BNC.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryMachineShutdowns, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    machineShutdowns.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(queryExternalShutdowns, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    externalShutdowns.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(querySeqCips, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    seqCips.Load(reader);
                    reader.Close();
                }

                using (SqlCommand command = new SqlCommand(querySeqCovs, connection))
                {
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@startYear", startYear);
                    command.Parameters.AddWithValue("@endYear", endYear);
                    reader = command.ExecuteReader();
                    seqCovs.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            IDictionary<string, DataTable> data = new Dictionary<string, DataTable>()
            {
                { "CIP", CIP },
                { "COV", COV },
                { "BNC", BNC },
                { "machines", machineShutdowns },
                { "external", externalShutdowns },
                { "seqCIP", seqCips },
                { "seqCOV", seqCovs }
            };

            return new JsonResult(data);
        }
        
    }
}