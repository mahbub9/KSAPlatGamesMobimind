using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class CampaignRepo : ICampaignRepo
    {
        private IBaseRepo _db;

        public CampaignRepo(IBaseRepo db)
        {
            _db = db;
        }

        public void AddFireBackLog(FireBackLog log)
        {
            _db.LContext.FireBackLogs.Add(log);
        }
    }
}
