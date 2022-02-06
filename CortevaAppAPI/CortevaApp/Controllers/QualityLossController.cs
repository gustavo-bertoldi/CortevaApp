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
    public class QualityLossController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public QualityLossController(IConfiguration configuration) => _configuration = configuration;

        [HttpGet("qualityLosses/{site}/{productionLine}/{beginningDate}/{endingDate}")]
        public JsonResult getQualityLossesPeriod(string site, string productionLine, string beginningDate, string endingDate)
        {
            IDictionary<string, DataTable> Results;
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

                    string QueryRejectionCounter = @"select sum(rc.fillercounter) as sumFillerCounter,
                                                     sum(rc.caperCounter) as sumCaperCounter, sum(rc.labelerCounter) as sumLabelerCounter,
                                                     sum(rc.weightBoxCounter) as sumWeightBoxCounter, sum(rc.fillerRejection) as sumFillerRejection,
                                                     sum(rc.caperRejection) as sumCaperRejection, sum(rc.labelerRejection) as sumLabelerRejection,
                                                     sum(rc.weightBoxRejection) as sumWeightBoxRejection, sum(qualityControlCounter) as sumQualityControlCounter,
                                                     sum(rc.qualityControlRejection) as sumQualityControlRejection
                                                     from dbo.ole_rejection_counters rc, dbo.ole_pos pos
                                                     where pos.number = rc.po
                                                     and pos.productionline_name = @productionLineName
                                                     and rc.created_at >= @startDate
                                                     and rc.created_at <= @endDate";

                    string QueryFormats = @"select *
                                            from dbo.ole_rejection_counters rc, dbo.ole_pos pos, dbo.ole_products prod
                                            where rc.po = pos.number
                                            and prod.GMID = pos.GMIDCode
                                            and pos.productionline_name = @productionLineName
                                            and rc.created_at >= @startDate
                                            and rc.created_at <= @endDate";

                    DataTable RejectionCounters = new DataTable();
                    DataTable Formats = new DataTable();

                    using (SqlCommand command = new SqlCommand(QueryRejectionCounter, connection))
                    {
                        command.Parameters.AddWithValue("@productionLineName", productionLine);
                        command.Parameters.AddWithValue("@startDate", beginningDate);
                        command.Parameters.AddWithValue("@endDate", endingDate);
                        reader = command.ExecuteReader();
                        RejectionCounters.Load(reader);
                        reader.Close();
                    }

                    using (SqlCommand command = new SqlCommand(QueryFormats, connection))
                    {
                        command.Parameters.AddWithValue("@productionLineName", productionLine);
                        command.Parameters.AddWithValue("@startDate", beginningDate);
                        command.Parameters.AddWithValue("@endDate", endingDate);
                        reader = command.ExecuteReader();
                        Formats.Load(reader);
                        reader.Close();
                    }

                    Results = new Dictionary<string, DataTable>()
                    {
                        { "rejectionCounter", RejectionCounters },
                        { "formats", Formats }
                    };

                }
                else
                {
                    Results = new Dictionary<string, DataTable>()
                    {
                        { "rejectionCounter", null },
                        { "formats", null }
                    };
                }
                connection.Close();
            }
            return new JsonResult(Results);
        }

        [HttpGet("performance/{po}")]
        public JsonResult GetPerformanceForASite(string po)
        {
            string QueryRRF = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                from dbo.ole_speed_losses sl
                                where sl.OLE = @po
                                and sl.reason = 'Reduced Rate At Filler'";

            string QueryRRM = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                from dbo.ole_speed_losses sl
                                where sl.OLE = @po
                                and sl.reason = 'Reduced Rate At An Other Machine'";

            string QueryFOS = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                from dbo.ole_speed_losses sl
                                where sl.OLE = @po
                                and sl.reason = 'Filler OWn Stoppage'";

            string QueryFSM = @"select sum(sl.duration) as Duration, count(*) as nbEvents
                                from dbo.ole_speed_losses sl
                                where sl.OLE = @po
                                and sl.reason = 'Filler Own Stoppage By An Other Machine'";

            DataTable RRF = new DataTable();
            DataTable RRM = new DataTable();
            DataTable FOS = new DataTable();
            DataTable FSM = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryRRF, connection))
                {
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    RRF.Load(reader);
                    reader.Close();
                }
                using (SqlCommand command = new SqlCommand(QueryRRM, connection))
                {
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    RRM.Load(reader);
                    reader.Close();
                }
                using (SqlCommand command = new SqlCommand(QueryFOS, connection))
                {
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    FOS.Load(reader);
                    reader.Close();
                }
                using (SqlCommand command = new SqlCommand(QueryFSM, connection))
                {
                    command.Parameters.AddWithValue("@po", po);
                    reader = command.ExecuteReader();
                    FSM.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            IDictionary<string, DataTable> Result = new Dictionary<string, DataTable>()
            {
              { "RRF", RRF },
              { "RRM", RRM },
              { "FOS", FOS },
              { "FSM", FSM },
            };

            return new JsonResult(Result);
        }

    }
}