using PlatGame.Data.IRepo;
using PlatGame.DataAccess.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.DataAccess.Repo
{
    public class StatusRepo : IStatusRepo
    {
        private IBaseRepo _db;

        public StatusRepo(IBaseRepo db)
        {
            _db = db;
        }

        public Guid GetStatusByCode(string code)
        {
            var data = _db.LContext.StatusMsgs.Where(x => x.Code == code).FirstOrDefault();
            if (data != null)
                return data.Id;
            return Guid.Empty;            
        }
    }
}
