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
    public class BillingBL : IBillingBL
    {
        private IBillingRepo _billingRepo;
        private IStatusRepo _statusRepo;
        private ISubscriptionRepo _subscriptionRepo;
        private ITransactionRepo _transactionRepo;
        private ISubscriptionBL _subBl;
        private ICampaignBL _campaignBL;

        public BillingBL(IBillingRepo billingRepo, IStatusRepo statusRepo,
                           ISubscriptionRepo subscriptionRepo, ITransactionRepo transactionRepo,
                           ISubscriptionBL subBl, ICampaignBL campaignBL)
        {
            _billingRepo = billingRepo;
            _statusRepo = statusRepo;
            _subscriptionRepo = subscriptionRepo;
            _transactionRepo = transactionRepo;
            _subBl = subBl;
            _campaignBL = campaignBL;
        }

        public bool AddFirstBilling(CallBackModel info, Guid transactionID)
        {
            var statusId = _statusRepo.GetStatusByCode(info.STATUS);
            // check if duplicate transaction
            //bool isDuplicate = _transactionRepo.IsDuplicateTransaction(info.MSISDN, statusId);

            // update is firstCharegd billing in sub table
            var res = _subBl.AddSubscriber(info, transactionID, true);
            if (res)
            {
                var billing = AddBilling(info, transactionID);
                if (billing)
                {
                    // check if the user subscribe before 
                    //bool isFirst = _subscriptionRepo.GetSubscriperByMsisdn(info.MSISDN) == null;
                    // add subscription in CM
                    //_campaignBL.InsertCampaignSubscribtion(transactionID.ToString(), info.MSISDN, info.Price, isFirst);
                    // add Mo in CM
                    _campaignBL.InsertCampaignDN(transactionID.ToString(), info.MSISDN, info.RequestID.ToString(), info.Price, 3);
                    // add Dn in CM
                    _campaignBL.InsertCampaignMO(transactionID.ToString(), info.MSISDN);

                    return true;
                }

            }

            return false;
        }

        public bool AddRenewal(CallBackModel info, Guid transactionID)
        {
            //var statusId = _statusRepo.GetStatusByCode(info.STATUS);
            // check if duplicate transaction
            //bool isDuplicate = _transactionRepo.IsDuplicateTransaction(info.MSISDN, statusId);

            var billing = AddBilling(info, transactionID);
            if (billing)
            {
                // add MT in CM
                _campaignBL.InsertCampaignMT(transactionID.ToString(), info.MSISDN, info.Price, true);
                // add Renewal in CM
                _campaignBL.InsertCampaignRenewal(transactionID.ToString(), info.MSISDN, info.Price);
                // add Mo in CM
                _campaignBL.InsertCampaignDN(transactionID.ToString(), info.MSISDN, info.RequestID.ToString(), info.Price, 3);
                // add Dn in CM
                _campaignBL.InsertCampaignMO(transactionID.ToString(), info.MSISDN);

                return true;
            }

            return false;
        }

        public bool AddFirstFailedBilling(CallBackModel info, Guid transactionID)
        {
            _campaignBL.InsertCampaignDN(transactionID.ToString(), info.MSISDN, info.RequestID.ToString(), info.Price, 4);
            return true;
        }

        public bool AddBilling(CallBackModel info, Guid transactionID)
        {
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(info.MSISDN);
            if (userInfo == null || userInfo.IsSubscriped == false)
                return false;
            Billing billing = new Billing
            {
                Price = info.Price,
                StatusID = _statusRepo.GetStatusByCode(info.STATUS),
                TransactionID = transactionID,
                SubscriberID = userInfo.Id
            };
            return _billingRepo.AddBilling(billing);
        }
    }
}
