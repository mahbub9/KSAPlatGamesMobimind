using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel
{
    public class CallBackModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int ServiceID { get; set; }
        public int ChannelID { get; set; }
        public int OperatorID { get; set; }
        public string RequestID { get; set; }
        public string MSISDN { get; set; }
        public string STATUS { get; set; }
        public decimal Price { get; set; }
        public string txid { get; set; }
        public string pubid { get; set; }
        public string affid { get; set; }
        public string pageid { get; set; }
    }
}
