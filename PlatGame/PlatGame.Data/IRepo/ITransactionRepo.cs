using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.IRepo
{
    public interface ITransactionRepo
    {
        Guid AddTransaction(Transaction info);
        bool IsDuplicateTransaction(string msisdn, Guid statusId);
    }
}
