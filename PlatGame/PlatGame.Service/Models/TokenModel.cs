using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Service.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int LifespanInSec { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
