using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private IBaseRepo _db;

        public SubscriptionRepo(IBaseRepo db)
        {
            _db = db;
        }

        public bool AddSubscriptionHistory(SubscriptionHistory info)
        {
            info.Id = Guid.NewGuid();
            info.CreatedDate = DateTime.Now;

            _db.LContext.SubscriptionHistories.Add(info);

            return _db.LContext.SaveChanges() > 0;
        }

        public bool AddSubscriper(Subscription info)
        {
            info.Id = Guid.NewGuid();
            info.CreatedDate = DateTime.Now;

            _db.LContext.Subscriptions.Add(info);

            return _db.LContext.SaveChanges() > 0;
        }

        public bool UpdateSubscriper(Subscription info)
        {           
            //_db.LContext.Subscriptions.Update(info);
            _db.LContext.Entry(info).State = System.Data.Entity.EntityState.Modified;
            

            return _db.LContext.SaveChanges() > 0;
        }

        public Subscription GetSubscriperByMsisdn(string msisdn)
        {
            return _db.LContext.Subscriptions.Where(x => x.Msisdn == msisdn).FirstOrDefault();
        }
    }
}
