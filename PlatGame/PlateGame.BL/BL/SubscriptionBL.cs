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
    public class SubscriptionBL : ISubscriptionBL
    {
        private ISubscriptionRepo _subscriptionRepo;
        private IStatusRepo _statusRepo;
        private IConversionRepo _conversionRepo;
        private ILandingRepo _landingRepo;
        private ICampaignBL _campaignBL;
        private ITelcoRepo _telcoRepo;

        public SubscriptionBL(ISubscriptionRepo subscriptionRepo, IStatusRepo statusRepo,
                             IConversionRepo conversionRepo, ILandingRepo landingRepo, ICampaignBL campaignBL,
                              ITelcoRepo telcoRepo)
        {
            _subscriptionRepo = subscriptionRepo;
            _statusRepo = statusRepo;
            _conversionRepo = conversionRepo;
            _landingRepo = landingRepo;
            _campaignBL = campaignBL;
            _telcoRepo = telcoRepo;
        }

        public bool AddSubscriptionHistory(CallBackModel info, Guid transactionID)
        {
            SubscriptionHistory subHistory = new SubscriptionHistory
            {
                Msisdn = info.MSISDN,
                Status = _statusRepo.GetStatusByCode(info.STATUS),
                TransactionID = transactionID
            };

            return _subscriptionRepo.AddSubscriptionHistory(subHistory);
        }

        public bool UnSubscribeUser(CallBackModel info, Guid transactionID)
        {
            var SubInfo = _subscriptionRepo.GetSubscriperByMsisdn(info.MSISDN);
            if (SubInfo != null)
            {
                if (SubInfo.IsSubscriped == true)
                {
                    // add to CM
                    _campaignBL.InsertCampaignUnSubscribtion(info.MSISDN, transactionID.ToString());

                    // add to sub history
                    AddSubscriptionHistory(info, transactionID);

                    // update subscriper status
                    SubInfo.IsSubscriped = false;
                    SubInfo.TransactionID = transactionID;
                    SubInfo.UnSubscriptionDate = DateTime.Now;
                    return _subscriptionRepo.UpdateSubscriper(SubInfo);
                }
                else
                    return true;
            }
            else
                return false;
        }

        public bool AddSubscriber(CallBackModel info, Guid transactionID, bool IsFirstBillingCharged = false)
        {
            // check if the user already exist in our db
            var SubInfo = _subscriptionRepo.GetSubscriperByMsisdn(info.MSISDN);

            // if SubInfo is null ---> new subscriper
            if (SubInfo == null)
            {

                // add to sub history
                AddSubscriptionHistory(info, transactionID);

                // add new subscriper
                Subscription sub = new Subscription
                {
                    IsSubscriped = true,
                    SubscriptionDate = DateTime.Now,
                    TransactionID = transactionID,
                    Msisdn = info.MSISDN,
                    TelcoID = info.OperatorID

                };

                if (IsFirstBillingCharged == true)
                    sub.IsFirstBillingCharged = true;

                if (info.txid != null)
                {
                    // add new conversion
                    _conversionRepo.AddConversion(info.MSISDN, info.txid);

                    // update landing to add msisdn
                    var landingInfo = _landingRepo.GetLandingByTxid(info.txid);
                    if (landingInfo != null)
                    {
                        landingInfo.Msisdn = info.MSISDN;
                        _landingRepo.UpdateLandingInfo(landingInfo);
                    }

                    // add txid to subscription table
                    sub.txid = info.txid;

                    // fireback to CM
                    // get telco info 
                    var telco = _telcoRepo.GetTelcoInfoById(info.OperatorID);
                    _campaignBL.FireBack(info.MSISDN, "", info.txid, telco.CMTelcoId);

                    // check if the user subscribe before 
                    bool isFirst = _subscriptionRepo.GetSubscriperByMsisdn(info.MSISDN) == null;
                    // add subscription in CM
                    _campaignBL.InsertCampaignSubscribtion(transactionID.ToString(), info.MSISDN, info.Price, isFirst);

                }

                return _subscriptionRepo.AddSubscriper(sub);

            }
            else // existing user 
            {
                // add to sub history
                AddSubscriptionHistory(info, transactionID);

                // update subscriper status
                SubInfo.IsSubscriped = true;
                SubInfo.TransactionID = transactionID;
                SubInfo.SubscriptionDate = DateTime.Now;
                SubInfo.TelcoID = info.OperatorID;

                _campaignBL.InsertCampaignSubscribtion(transactionID.ToString(), info.MSISDN, info.Price, false);

                if (IsFirstBillingCharged == true)
                    SubInfo.IsFirstBillingCharged = true;

                return _subscriptionRepo.UpdateSubscriper(SubInfo);
            }

        }
    }
}
