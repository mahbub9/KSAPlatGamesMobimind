using System;
using SharedModel;

namespace PlateGame.BL.IBL
{
    public interface ITransactionBL
    {
        Guid AddTransaction(CallBackModel info);
        bool IsDuplicateTransaction(string msisdn, Guid statusId);
    }
}
