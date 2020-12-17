using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlatGames.Models
{
    public class CallBackModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        [Required]
        public int ServiceID { get; set; }
        [Required]
        public int ChannelID { get; set; }
        [Required]
        public int OperatorID { get; set; }
        [Required]
        public string RequestID { get; set; }
        [Required]
        public string MSISDN { get; set; }
        [Required]
        public string STATUS { get; set; }
        public decimal? Price { get; set; } = 0;
        public string txid { get; set; }
        public string pubid { get; set; }
        public string affid { get; set; }
        public string pageid { get; set; }
        public string Lsource { get; set; } = "";
        public string Ldate { get; set; } = DateTime.Now.ToString();
        public int CGStatus { get; set; } = -1;
    }
}