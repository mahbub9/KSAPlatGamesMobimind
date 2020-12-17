using PlateGame.BL.IBL;
using PlatGame.Data;
using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.BL
{
    public class LandingBL : ILandingBL
    {
        private ILandingRepo _landingRepo;

        public LandingBL(ILandingRepo landingRepo)
        {
            _landingRepo = landingRepo;
        }

        public bool AddLanding(LandingIP info)
        {
            return _landingRepo.AddLanding(info);
        }

        public LandingIP GetLandingByTxid(string txid)
        {
            return _landingRepo.GetLandingByTxid(txid);
        }
    }
}
