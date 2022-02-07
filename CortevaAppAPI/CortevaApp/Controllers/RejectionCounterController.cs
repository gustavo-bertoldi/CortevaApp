using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CortevaApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CortevaApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class RejectionCounterController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public RejectionCounterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("storeRejection")]
        public JsonResult StoreRejection(RejectionCounters rc)
        {
            string QueryNewStoreRejection = @"insert into dbo.ole_rejection_counters (po, fillerCounter, caperCounter,
                                              labelerCounter, weightBoxCounter, qualityControlCounter,
                                              fillerRejection, caperRejection, labelerRejection, weightBoxRejection,
                                              qualityControlRejection)
                                              values (@Po, @FillerCounter, @CaperCounter, @LabelerCounter, @WeightBoxCounter,
                                              @QualityControlCounter, @FillerRejection, @CaperRejection, @LabelerRejection,
                                              @WeightBoxRejection, @QualityControlRejection)";


            DataTable NewStoreRejection = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("CortevaDBConnection");
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryNewStoreRejection, connection))
                {
                    command.Parameters.AddWithValue("@Po", rc.po);
                    command.Parameters.AddWithValue("@FillerCounter", rc.fillerCounter);
                    command.Parameters.AddWithValue("@CaperCounter", rc.caperCounter);
                    command.Parameters.AddWithValue("@LabelerCounter", rc.labelerCounter);
                    command.Parameters.AddWithValue("@WeightBoxCounter", rc.weightBoxCounter);
                    command.Parameters.AddWithValue("@QualityControlCounter", rc.qualityControlCounter);
                    command.Parameters.AddWithValue("@FillerRejection", rc.fillerRejection);
                    command.Parameters.AddWithValue("@CaperRejection", rc.caperRejection);
                    command.Parameters.AddWithValue("@LabelerRejection", rc.labelerRejection);
                    command.Parameters.AddWithValue("@WeightBoxRejection", rc.weightBoxRejection);
                    command.Parameters.AddWithValue("@QualityControlRejection", rc.qualityControlRejection);
                    reader = command.ExecuteReader();
                    NewStoreRejection.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }

            return new JsonResult(NewStoreRejection);
        }
    }
}