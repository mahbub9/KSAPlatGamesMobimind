using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class TelcoRepo : ITelcoRepo
    {
        private IBaseRepo _db;

        public TelcoRepo(IBaseRepo db)
        {
            _db = db;
        }

        public Telco GetTelcoInfoById(int id)
        {
            return _db.LContext.Telcoes.Where(x => x.TelcoId == id).FirstOrDefault();
        }
    }
}
