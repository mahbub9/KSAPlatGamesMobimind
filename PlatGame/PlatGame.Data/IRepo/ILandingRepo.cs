using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatGame.DataAccess;

namespace PlatGame.Data.IRepo
{
    public interface ILandingRepo
    {
        bool AddLanding(LandingIP info);
        LandingIP GetLandingByTxid(string txid);
        bool UpdateLandingInfo(LandingIP info);
    }
}
