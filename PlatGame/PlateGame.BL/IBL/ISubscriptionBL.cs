using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface ISubscriptionBL
    {
        bool AddSubscriber(CallBackModel info, Guid transactionID, bool IsFirstBillingCharged = false);
        bool AddSubscriptionHistory(CallBackModel info, Guid transactionID);
        bool UnSubscribeUser(CallBackModel info, Guid transactionID);
    }
}
