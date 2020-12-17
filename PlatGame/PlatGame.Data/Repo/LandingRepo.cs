using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class LandingRepo : ILandingRepo
    {
        private IBaseRepo _db;

        public LandingRepo(IBaseRepo db)
        {
            _db = db;
        }

        public bool AddLanding(LandingIP info)
        {
            info.Id = Guid.NewGuid();
            info.CreatedDate = DateTime.Now;

            _db.LContext.LandingIPs.Add(info);

            return _db.LContext.SaveChanges() > 0;
        }

        public LandingIP GetLandingByTxid(string txid)
        {
            return _db.LContext.LandingIPs.Where(x => x.txid == txid).FirstOrDefault();
        }

        public bool UpdateLandingInfo(LandingIP info)
        {
            //_db.LContext.LandingIPs.Update(info);
            _db.LContext.Entry(info).State = System.Data.Entity.EntityState.Modified;

            return _db.LContext.SaveChanges() > 0;
        }
    }
}
