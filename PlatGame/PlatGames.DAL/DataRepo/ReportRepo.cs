using ForestInterActive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using System.Linq.Expressions;

namespace PlatGames.DAL.DataRepo
{
    public class ReportRepo : BaseRepo
    {
        public List<GetRevenueReport_Result> GetRevenueReport(DateTime from, DateTime to)
        {
            return LContext.GetRevenueReport(from, to).ToList();
        }
        public List<TransactionSummaryReport_Result> TransactionSummaryReport(DateTime from, DateTime to)
        {
            return LContext.TransactionSummaryReport(from, to).ToList();
        }
        public List<SubscriptionInfo_Result> SubscriptionInfoReport(DateTime from, DateTime to)
        {
            return LContext.SubscriptionInfo(from, to).ToList();
        }
    }
}
