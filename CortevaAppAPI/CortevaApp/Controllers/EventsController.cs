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
                { "seqCOV", seqCovs } ,
            };

            return new JsonResult(data);
        }




       
        [HttpGet("allevents/{site}/{productionLine}/{beginningDate}/{endingDate}")]
        public JsonResult GetAllEventsPeriod(string site, string productionLine, string beginningDate, string endingDate)
        {
            string QuerySites = @"select *
                                from dbo.ole_productionline pl, dbo.worksite w, dbo.ole_pos pos,
                                dbo.ole_products prod, dbo.ole_rejection_counters rc
                                where w.name = pl.worksite_name
                                and pos.productionline_name = pl.productionline_name
                                and pos.GMIDCode = prod.GMID
                                and rc.po = pos.number
                                and w.name = @site
                                and pl.productionline_name = @productionLine
                                and pos.created_at >= @beginningDate
                                and pos.created_at <= @endingDate";

            DataTable Sites = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySites, connection))
                {
                    command.Parameters.AddWithValue("@site", site);
                    command.Parameters.AddWithValue("@productionLine", productionLine);
                    command.Parameters.AddWithValue("@beginningDate", beginningDate);
                    command.Parameters.AddWithValue("@endingDate", endingDate);
                    reader = command.ExecuteReader();
                    Sites.Load(reader);
                    reader.Close();
                }

                if (Sites.Rows.Count > 0)
                {
                    string[] _begDate = beginningDate.Split('-');
                    int beginningYear = Int16.Parse(_begDate[0]);
                    int beginningMonth = Int16.Parse(_begDate[1]);
                    int beginningDay = Int16.Parse(_begDate[2]);

                    string[] _endDate = endingDate.Split('-');
                    int endingYear = Int16.Parse(_endDate[0]);
                    int endingMonth = Int16.Parse(_endDate[1]);
                    int endingDay = Int16.Parse(_endDate[2]);

                    string startDate = beginningYear + "-" + beginningMonth + "-" + beginningDay + " 00:00:00.000";
                    string endDate = endingYear + "-" + endingMonth + "-" + endingDay + " 00:00:00.000";

                    string QuerySpeedLossesEvents = @"select sl.duration, sl.reason, sl.comment, pos.id, pos.qtyProduced,
                                                      pos.workingDuration, prod.size, prod.idealRate
                                                      from dbo.ole_speed_losses sl, dbo.ole_pos pos, dbo.ole_products prod
                                                      where pos.number = sl.OLE
                                                      and prod.GMID = pos.GMIDCode
                                                      and sl.productionline = @productionlineName
                                                      and sl.created_at >= @startDate
                                                      and sl.created_at <= @endDate";

                    string QueryBM = @"select sum(pe.duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_planned_events pe
                                       where pe.productionline = @productionlineName
                                       and pe.created_at >= @startDate
                                       and pe.created_at <= @endDate
                                       and (pe.reason = 'Pause' or pe.reason = 'Reunion'
                                       or pe.reason = 'Pas de production prevue')";

                    string QueryCP = @"select sum(pe.duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_planned_events pe
                                       where pe.productionline = @productionlineName
                                       and pe.created_at >= @startDate
                                       and pe.created_at <= @endDate
                                       and pe.reason = 'Implementation de projet'";

                    string QueryPM = @"select sum(pe.duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_planned_events pe
                                       where pe.productionline = @productionlineName
                                       and pe.created_at >= @startDate
                                       and pe.created_at <= @endDate
                                       and pe.reason = 'Maintenance'";

                    string QueryPP = @"select sum(pe.duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_planned_events pe
                                       where pe.productionline = @productionlineName
                                       and pe.created_at >= @startDate
                                       and pe.created_at <= @endDate
                                       and pe.reason = 'Pas de production prevue'";

                    string QueryCIP = @"select sum(cip.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_cips cip
                                       where cip.productionline = @productionlineName
                                       and cip.created_at >= @startDate
                                       and cip.created_at <= @endDate";

                    string QueryCOV = @"select sum(cov.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_changing_clients cov
                                       where cov.productionline = @productionlineName
                                       and cov.created_at >= @startDate
                                       and cov.created_at <= @endDate";

                    string QueryBNC = @"select sum(bnc.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_changing_formats bnc
                                       where bnc.productionline = @productionlineName
                                       and bnc.created_at >= @startDate
                                       and bnc.created_at <= @endDate";

                    string QueryUEE = @"select sum(ud.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_unplanned_downtimes ud
                                       where ud.productionline = @productionlineName
                                       and ud.created_at >= @startDate
                                       and ud.created_at <= @endDate
                                       and ud.component = 'other'";

                    string QueryUSM = @"select sum(ud.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_unplanned_downtimes ud
                                       where ud.productionline = @productionlineName
                                       and ud.created_at >= @startDate
                                       and ud.created_at <= @endDate
                                       and (ud.component != 'downstreamSaturation' or ud.component != 'missingBottle'
                                       or ud.component != 'other')";

                    string QueryFUS = @"select sum(ud.total_duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_unplanned_event_unplanned_downtimes ud
                                       where ud.productionline = @productionlineName
                                       and ud.created_at >= @startDate
                                       and ud.created_at <= @endDate
                                       and (ud.component = 'downstreamSaturation' or ud.component = 'missingBottle')";

                    string QueryRRF = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                       from dbo.ole_speed_losses sl
                                       where sl.productionline = @productionlineName
                                       and sl.created_at >= @startDate
                                       and sl.created_at <= @endDate
                                       and sl.reason = 'Reduced Rate At Filler'";

                    string QueryRRFMonth = @"select *
                                            from dbo.ole_speed_losses sl
                                            where sl.productionline = @productionlineName
                                            and sl.created_at >= @startDate
                                            and sl.created_at <= @endDate
                                            and sl.reason = 'Reduced Rate At Filler'";

                    string QueryRRMMonth = @"select *
                                            from dbo.ole_speed_losses sl
                                            where sl.productionline = @productionlineName
                                            and sl.created_at >= @startDate
                                            and sl.created_at <= @endDate
                                            and sl.reason = 'Reduced Rate At An Other Machine'";

                    string QueryFOSMonth = @"select *
                                            from dbo.ole_speed_losses sl
                                            where sl.productionline = @productionlineName
                                            and sl.created_at >= @startDate
                                            and sl.created_at <= @endDate
                                            and sl.reason = 'Filler Own Stoppage'";

                    string QueryFSMMonth = @"select *
                                            from dbo.ole_speed_losses sl
                                            where sl.productionline = @productionlineName
                                            and sl.created_at >= @startDate
                                            and sl.created_at <= @endDate
                                            and sl.reason = 'Filler Own Stoppage By An Other Machine'";

                    string QueryRRM = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                        from dbo.ole_speed_losses sl
                                        where sl.productionline = @productionlineName
                                        and sl.created_at >= @startDate
                                        and sl.created_at <= @endDate
                                        and sl.reason = 'Reduced Rate At An Other Machine'";

                    string QueryFOS = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                        from dbo.ole_speed_losses sl
                                        where sl.productionline = @productionlineName
                                        and sl.created_at >= @startDate
                                        and sl.created_at <= @endDate
                                        and sl.reason = 'Filler Own Stoppage'";

                    string QueryFSM = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                        from dbo.ole_speed_losses sl
                                        where sl.productionline = @productionlineName
                                        and sl.created_at >= @startDate
                                        and sl.created_at <= @endDate
                                        and sl.reason = 'Filler Own Stoppage By An Other Machine'";

                    string QueryPlannedEvents = @"select *
                                                  from dbo.ole_planned_events pe
                                                  where pe.productionline = @productionlineName
                                                  and pe.created_at >= @startDate
                                                  and pe.created_at <= @endDate";

                    string QueryChangingClients = @"select *
                                                  from dbo.ole_unplanned_event_changing_clients cov
                                                  where cov.productionline = @productionlineName
                                                  and cov.created_at >= @startDate
                                                  and cov.created_at <= @endDate";

                    string QueryCIPBis = @"select *
                                           from dbo.ole_unplanned_event_cips cip
                                           where cip.productionline = @productionlineName
                                           and cip.created_at >= @startDate
                                           and cip.created_at <= @endDate";

                    string QueryUnplanned = @"select *
                                           from dbo.ole_unplanned_event_unplanned_downtimes ud
                                           where ud.productionline = @productionlineName
                                           and ud.created_at >= @startDate
                                           and ud.created_at <= @endDate";

                    string QueryChangingFormats = @"select *
                                                   from dbo.ole_unplanned_event_changing_formats bnc
                                                   where bnc.productionline = @productionlineName
                                                   and bnc.created_at >= @startDate
                                                   and bnc.created_at <= @endDate";


                    IDictionary<string, DataTable> Queries = new Dictionary<string, DataTable>()
                    {
                        { QuerySpeedLossesEvents, new DataTable() },
                        { QueryBM, new DataTable() },
                        { QueryCP, new DataTable() },
                        { QueryPM, new DataTable() },
                        { QueryPP, new DataTable() },
                        { QueryCIP, new DataTable() },
                        { QueryCOV, new DataTable() },
                        { QueryBNC, new DataTable() },
                        { QueryUEE, new DataTable() },
                        { QueryUSM, new DataTable() },
                        { QueryFUS, new DataTable() },
                        { QueryRRF, new DataTable() },
                        { QueryRRFMonth, new DataTable() },
                        { QueryRRMMonth, new DataTable() },
                        { QueryFOSMonth, new DataTable() },
                        { QueryFSMMonth, new DataTable() },
                        { QueryRRM, new DataTable() },
                        { QueryFOS, new DataTable() },
                        { QueryFSM, new DataTable() },
                        { QueryPlannedEvents, new DataTable() },
                        { QueryChangingClients, new DataTable() },
                        { QueryCIPBis, new DataTable() },
                        { QueryUnplanned, new DataTable() },
                        { QueryChangingFormats, new DataTable() }
                    };

                    foreach (KeyValuePair<string, DataTable> Entry in Queries)
                    {
                        using (SqlCommand command = new SqlCommand(Entry.Key, connection))
                        {
                            command.Parameters.AddWithValue("@productionlineName", productionLine);
                            command.Parameters.AddWithValue("@startDate", startDate);
                            command.Parameters.AddWithValue("@endDate", endDate);
                            reader = command.ExecuteReader();
                            Entry.Value.Load(reader);
                            reader.Close();
                        }
                    }

                    connection.Close();
                    return new JsonResult(new Dictionary<string, DataTable>()
                    {
                        { "BM", Queries[QueryBM] },
                        { "CP", Queries[QueryCP] },
                        { "PM", Queries[QueryPM] },
                        { "PP", Queries[QueryPP] },
                        { "CIP", Queries[QueryCIP] },
                        { "COV", Queries[QueryCOV] },
                        { "BNC", Queries[QueryBNC] },
                        { "UEE", Queries[QueryUEE] },
                        { "USM", Queries[QueryUSM] },
                        { "FUS", Queries[QueryFUS] },
                        { "RRF", Queries[QueryRRF] },
                        { "RRM", Queries[QueryRRF] },
                        { "FOS", Queries[QueryFOS] },
                        { "FSM", Queries[QueryFSM] },
                        { "SITE", Sites },
                        { "EVENTS", Queries[QueryChangingFormats] },
                        { "SLEVENTS", Queries[QuerySpeedLossesEvents] },
                        { "PLANNEDEVENTS", Queries[QueryPlannedEvents] },
                        { "RRFMonth", Queries[QueryRRFMonth] },
                        { "RRMMonth", Queries[QueryRRMMonth] },
                        { "FOSMonth", Queries[QueryFOSMonth] },
                        { "FSMMonth", Queries[QueryFSMMonth] }
                    });
                }
                else
                {
                    connection.Close();
                    return new JsonResult(new Dictionary<string, int>()
                    {
                        { "BM", 0 },
                        { "CP", 0 },
                        { "PM", 0 },
                        { "PP", 0 },
                        { "CIP", 0 },
                        { "COV", 0 },
                        { "BNC", 0 },
                        { "UEE", 0 },
                        { "USM", 0 },
                        { "FUS", 0 },
                        { "RRF", 0 },
                        { "RRM", 0 },
                        { "FOS", 0 },
                        { "FSM", 0 },
                        { "SITE", 0 },
                        { "EVENTS", 0 }
                    } );
                }
            }
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


        [HttpPost("assignation")]
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

        [HttpPost("unplannedEvent/changingFormat")]
        public JsonResult SaveUnplannedEventChangingFormat(UnplannedEventChangingFormat ue)
        {
            string QuerySaveUECF = @"insert into dbo.ole_unplanned_event_changing_formats
                                   (OLE, productionline, predicted_duration, total_duration, comment)
                                   values (@OLE, @PL, @PD, @TD, @C)";


            DataTable SaveUECF = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySaveUECF, connection))
                {
                    command.Parameters.AddWithValue("@OLE", ue.OLE);
                    command.Parameters.AddWithValue("@PL", ue.productionline);
                    command.Parameters.AddWithValue("@PD", ue.predicted_duration);
                    command.Parameters.AddWithValue("@TD", ue.total_duration);
                    command.Parameters.AddWithValue("@C", ue.comment);
                    reader = command.ExecuteReader();
                    SaveUECF.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            return new JsonResult(SaveUECF);
        }

        [HttpPost("unplannedEvent/clientChanging")]
        public JsonResult SaveUnplannedEventChangingClient(UnplannedEventChangingClient ue)
        {
            string QuerySaveUECC = @"insert into dbo.ole_unplanned_event_changing_clients
                                   (OLE, productionline, predicted_duration, total_duration, comment, lot_number)
                                   values (@OLE, @PL, @PD, @TD, @C, @LN)";


            DataTable SaveUECC = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySaveUECC, connection))
                {
                    command.Parameters.AddWithValue("@OLE", ue.OLE);
                    command.Parameters.AddWithValue("@PL", ue.productionline);
                    command.Parameters.AddWithValue("@PD", ue.predicted_duration);
                    command.Parameters.AddWithValue("@TD", ue.total_duration);
                    command.Parameters.AddWithValue("@C", ue.comment);
                    command.Parameters.AddWithValue("@LN", ue.lot_number);
                    reader = command.ExecuteReader();
                    SaveUECC.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            return new JsonResult(SaveUECC);
        }

        [HttpPost("unplannedEvent/CIP")]
        public JsonResult SaveUnplannedEventCIP(UnplannedEventCIP ue)
        {
            string QuerySaveUECIP = @"insert into dbo.ole_unplanned_event_cips
                                   (OLE, productionline, predicted_duration, total_duration, comment, previous_bulk)
                                   values (@OLE, @PL, @PD, @TD, @C, @PB)";


            DataTable SaveUECIP = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySaveUECIP, connection))
                {
                    command.Parameters.AddWithValue("@OLE", ue.OLE);
                    command.Parameters.AddWithValue("@PL", ue.productionline);
                    command.Parameters.AddWithValue("@PD", ue.predicted_duration);
                    command.Parameters.AddWithValue("@TD", ue.total_duration);
                    command.Parameters.AddWithValue("@C", ue.comment);
                    command.Parameters.AddWithValue("@PB", ue.previous_bulk);
                    reader = command.ExecuteReader();
                    SaveUECIP.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            return new JsonResult(SaveUECIP);
        }

        [HttpPost("unplannedEvent/unplannedDowntime")]
        public JsonResult SaveUnplannedEventUnplannedDowntime(UnplannedEventUnplannedDowntime ue)
        {
            string QuerySaveUEUD = @"insert into dbo.ole_unplanned_event_unplanned_downtimes
                                   (OLE, productionline, implicated_machine, total_duration, comment, component)
                                   values (@OLE, @PL, @IM, @TD, @COMM, @COMP)";


            DataTable SaveUEUD = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySaveUEUD, connection))
                {
                    command.Parameters.AddWithValue("@OLE", ue.OLE);
                    command.Parameters.AddWithValue("@PL", ue.productionline);
                    command.Parameters.AddWithValue("@COMP", ue.component);
                    command.Parameters.AddWithValue("@TD", ue.total_duration);
                    command.Parameters.AddWithValue("@COMM", ue.comment);
                    command.Parameters.AddWithValue("@IM", ue.implicated_machine);
                    reader = command.ExecuteReader();
                    SaveUEUD.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            return new JsonResult(SaveUEUD);
        }

        [HttpPost("plannedEvent")]
        public JsonResult SavePlannedEvent(PlannedEvent pe)
        {
            string QuerySavePE = @"insert into dbo.ole_planned_events
                                   (OLE, productionline, reason, duration, comment)
                                   values (@OLE, @PL, @R, @D, @COMM)";


            DataTable SavePE = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySavePE, connection))
                {
                    command.Parameters.AddWithValue("@OLE", pe.OLE);
                    command.Parameters.AddWithValue("@PL", pe.productionline);
                    command.Parameters.AddWithValue("@R", pe.reason);
                    command.Parameters.AddWithValue("@D", pe.duration);
                    command.Parameters.AddWithValue("@COMM", pe.comment);
                    reader = command.ExecuteReader();
                    SavePE.Load(reader);
                    reader.Close();
                }


                connection.Close();
            }

            return new JsonResult(SavePE);
        }
    }
}