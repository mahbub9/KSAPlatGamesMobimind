using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.IRepo
{
    public interface IConversionRepo
    {
        bool AddConversion(string msisdn, string txid);
    }
}
