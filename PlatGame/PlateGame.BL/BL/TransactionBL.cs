using PlateGame.BL.IBL;
using PlatGame.Data;
using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using PlatGame.DataAccess.IRepo;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.BL
{
    public class TransactionBL : ITransactionBL
    {
        private ITransactionRepo _transactionRepo;
        private IStatusRepo _status;

        public TransactionBL(ITransactionRepo transactionRepo, IStatusRepo status)
        {
            _transactionRepo = transactionRepo;
            _status = status;
        }

        public bool IsDuplicateTransaction(string msisdn, Guid statusId)
        {
            return _transactionRepo.IsDuplicateTransaction(msisdn, statusId);
        }

        public Guid AddTransaction(CallBackModel info)
        {
            Transaction tran = new Transaction
            {
                ChannelID = info.ChannelID,
                MSISDN = info.MSISDN,
                OperatorID = info.OperatorID,
                ServiceID = info.ServiceID,
                Password = info.Password,
                RequestID = info.RequestID,
                STATUS = _status.GetStatusByCode(info.STATUS),
                User = info.User,
                Price = info.Price

            };

            if (info.pubid != null)
            {
                string[] sanatizePubid = info.pubid.Split('_');
                string sanatizedPubid = sanatizePubid[0] + "_" + sanatizePubid[1];
                tran.AffId = info.affid;
                tran.PageId = info.pageid;
                tran.PubId = sanatizedPubid;
            }


            return _transactionRepo.AddTransaction(tran);
        }
    }
}
