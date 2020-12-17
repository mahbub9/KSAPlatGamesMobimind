using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface IBillingBL
    {
        bool AddBilling(CallBackModel info, Guid transactionID);
        bool AddFirstBilling(CallBackModel info, Guid transactionID);
        bool AddFirstFailedBilling(CallBackModel info, Guid transactionID);
        bool AddRenewal(CallBackModel info, Guid transactionID);
    }
}
