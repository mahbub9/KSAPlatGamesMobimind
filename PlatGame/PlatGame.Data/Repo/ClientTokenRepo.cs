using PlatGame.Data.IRepo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class ClientTokenRepo : IClientTokenRepo
    {
        private IBaseRepo _db;
        public ClientTokenRepo(IBaseRepo db)
        {
            _db = db;
        }
        public bool AddClientToken(ClientToken clientToken)
        {
            _db.LContext.ClientTokens.Add(clientToken);

            return _db.LContext.SaveChanges() > 0;
        }       

        public bool IsValidTokenExists()
        {
            //return _db.LContext.ClientTokens.Any(x => x.DateCreated.AddSeconds(x.LifeSpanInSec) > DateTime.Now) ;
            return _db.LContext.ClientTokens.Any(x => DbFunctions.AddSeconds(x.DateCreated, 43200) > DateTime.Now);

        }

        public ClientToken GetToken()
        {
            return _db.LContext.ClientTokens.FirstOrDefault();
        }
    }
}
