using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class BillingRepo : IBillingRepo
    {
        private IBaseRepo _db;

        public BillingRepo(IBaseRepo db)
        {
            _db = db;
        }

        public bool AddBilling(Billing info)
        {
            info.BillingID = Guid.NewGuid();
            info.CreatedDate = DateTime.Now;

            _db.LContext.Billings.Add(info);

            return _db.LContext.SaveChanges() > 0;
        }
    }
}
