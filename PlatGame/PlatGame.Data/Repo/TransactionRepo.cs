using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class TransactionRepo : ITransactionRepo
    {
        private IBaseRepo _db;

        public TransactionRepo(IBaseRepo db)
        {
            _db = db;
        }

        public Guid AddTransaction(Transaction info)
        {
            info.Id = Guid.NewGuid();
            info.CreatedDate = DateTime.Now;

            _db.LContext.Transactions.Add(info);

            if (_db.LContext.SaveChanges() > 0)
                return info.Id;
            return Guid.Empty;
        }

        public bool IsDuplicateTransaction(string msisdn, Guid statusId)
        {
            return _db.LContext.Transactions.Where(x => x.MSISDN == msisdn &&
                                                        x.STATUS == statusId &&
                                                        x.CreatedDate.Date == DateTime.Now.Date
                                                        )
                                            .FirstOrDefault() != null;
        }
    }
}
