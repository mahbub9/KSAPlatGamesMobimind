using PlatGame.Data;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface ILandingBL
    {
        bool AddLanding(LandingIP info);
        LandingIP GetLandingByTxid(string txid);
    }
}
