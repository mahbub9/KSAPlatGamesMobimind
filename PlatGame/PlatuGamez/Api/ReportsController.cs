using ForestInterActive;
using PlatGames.DAL;
using PlatGames.DAL.DataRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PlatGame.Api
{
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        static readonly ReportRepo reportRepository = new ReportRepo();

        [Route("GetRevenueReport")]
        [HttpGet]
        public IHttpActionResult GetRevenueReport(DateTime FromDt, DateTime ToDt, int? telcoId=0)
        {
            if (FromDt == null || ToDt == null)
            {
                return BadRequest("Missing Parameters");
            }

            try
            {
                List<GetRevenueReport_Result> revenueReport = reportRepository.GetRevenueReport(FromDt, ToDt);
                if (telcoId.HasValue && telcoId != 0 && revenueReport != null)
                {
                    revenueReport = revenueReport.Where(c => c.TelcoID == telcoId).ToList();
                }
                return Ok(revenueReport);

            }
            catch (Exception ex)
            {
                ForestInterActive.Logs.Log(ex,"RevReport");
            }
            return Ok();
        }

        [Route("GetTransactionSummaryReport")]
        [HttpGet]
        public IHttpActionResult GetTransactionSummaryReport(DateTime FromDt, DateTime ToDt)
        {
            if (FromDt == null || ToDt == null)
            {
                return BadRequest("Missing Parameters");
            }

            List<TransactionSummaryReport_Result> transactionSummaryReport = reportRepository.TransactionSummaryReport(FromDt, ToDt);
            return Ok(transactionSummaryReport);
        }

        [Route("GetSubscriptionInfoReport")]
        [HttpGet]
        public IHttpActionResult GetSubscriptionInfoReport(DateTime FromDt, DateTime ToDt)
        {
            if (FromDt == null || ToDt == null)
            {
                return BadRequest("Missing Parameters");
            }

            List<SubscriptionInfo_Result> subscriptionInfoReport = reportRepository.SubscriptionInfoReport(FromDt, ToDt);
            return Ok(subscriptionInfoReport);
        }
    }
}