using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.IRepo
{
    public interface ISubscriptionRepo
    {
        bool AddSubscriper(Subscription info);
        bool AddSubscriptionHistory(SubscriptionHistory info);
        Subscription GetSubscriperByMsisdn(string msisdn);
        bool UpdateSubscriper(Subscription info);
    }
}
