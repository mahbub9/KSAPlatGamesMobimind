using PlatGames.DAL;
using PlatGames.DAL.DataRepo;
using PlatGames.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlatGames.BL
{
    public class SubscriptionLogic
    {
        public LandingClick AddLanding(CallBackModel callBackModel)
        {
            LandingClick landingClick = new LandingClick();
            landingClick.Txid = callBackModel.txid;
            landingClick.Affid = callBackModel.affid;
            landingClick.LandingSource = callBackModel.Lsource;
            landingClick.PubId = callBackModel.pubid;
            landingClick.LandingDate = DateTime.Now;//DateTime.Parse(callBackModel.Ldate);
            new LandingClickRepo().Insert(landingClick);
            return landingClick;
        }
        public Result InsertSubscrbtion(CallBackModel callBackModel, Subscription subscription, int telcoid)
        {
            //subscription.Id = Guid.NewGuid();
            subscription.Msisdn = callBackModel.MSISDN;
            subscription.IsSubscribed = true;
            subscription.LastCharegDate = DateTime.Now;
            subscription.RenewalDate = (callBackModel.ChannelID == 12029 || callBackModel.ChannelID == 12030) ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(1);
            subscription.RenewalSent = false;
            subscription.SubMethod = callBackModel.Lsource.StartsWith("portal") ? "portal" : "campaign";
            subscription.SubscribedBy = callBackModel.Lsource;
            subscription.SubscriptionDate = DateTime.Now;
            subscription.TelcoId = telcoid;
            subscription.Txid = callBackModel.txid;
            subscription.IsCampaign = string.IsNullOrEmpty(callBackModel.txid) ? false : true;
            return new SubscriptionRepo().Save(subscription);
        }
        public Result ActiveSubscriber(Subscription subscription)
        {
            subscription.IsSubscribed = true;
            subscription.RenewalSent = true;
            subscription.RenewalDate = DateTime.Now;
            subscription.LastCharegDate = DateTime.Now;
            return new SubscriptionRepo().Save(subscription);
        }
        public Result UnSubscrbtion(CallBackModel createResponce, Subscription subscription, string TerminatedBy, int telcoid)
        {
            subscription.IsSubscribed = false;
            subscription.TerminationMethod = createResponce.STATUS;
            subscription.TerminatedBy = TerminatedBy;
            subscription.TerminationDate = DateTime.Now;
            subscription.TelcoId = telcoid;
            return new SubscriptionRepo().Save(subscription);
        }
        public Result InsertSubscrbtionHistory(Subscription subscription)
        {
            SubscriptionHistory subscriptionHistory = new SubscriptionHistory();
            subscriptionHistory.Id = Guid.NewGuid();
            subscriptionHistory.IsCampaign = subscription.IsCampaign;
            subscriptionHistory.IsSubscribed = subscription.IsSubscribed;
            subscriptionHistory.LastCharegDate = subscription.LastCharegDate;
            subscriptionHistory.RenewalDate = subscription.RenewalDate;
            subscriptionHistory.RenewalSent = subscription.RenewalSent;
            subscriptionHistory.SubMethod = subscription.SubMethod;
            subscriptionHistory.SubscribedBy = subscription.SubscribedBy;
            subscriptionHistory.SubscriptionDate = subscription.SubscriptionDate;
            subscriptionHistory.SubscriptionId = subscription.Id;
            subscriptionHistory.TelcoId = subscription.TelcoId;
            subscriptionHistory.TerminatedBy = subscription.TerminatedBy;
            subscriptionHistory.TerminationDate = subscription.TerminationDate;
            subscriptionHistory.TerminationMethod = subscription.TerminationMethod;
            subscriptionHistory.Txid = subscription.Txid;
            subscriptionHistory.Msisdn = subscription.Msisdn;
            return new SubscriptionHistoryRepo().Insert(subscriptionHistory);
        }
        public Result InsertTransaction(Subscription subscription, CallBackModel createResponce, int type, int telcoid)
        {
            Transaction transaction = new TransactionsRepo().FindBy(c => c.SubscriptionId == subscription.Id && c.TypeID == type && DbFunctions.TruncateTime(c.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
            if (transaction != null)
            {
                return new Result(ResultState.Fail, "Duplicate Transaction", transaction);
            }
            transaction =
            new Transaction
            {
                Id = Guid.NewGuid(),
                Msisdn = createResponce.MSISDN,
                ChannelID = createResponce.ChannelID,
                OperatorID = createResponce.OperatorID,
                //Price = createResponce.Price.HasValue ? createResponce.Price.Value / 100 : 0,
                Price = createResponce.Price.HasValue ? createResponce.Price.Value : 0,
                RequestID = createResponce.RequestID,
                SubscriptionId = subscription.Id,
                TelcoId = telcoid,
                TypeID = type,
                Date = DateTime.Now
            };

            //transaction.Price = createResponce.Price.HasValue ? createResponce.Price.Value / 100 : 0;
            transaction.Price = createResponce.Price.HasValue ? createResponce.Price.Value : 0;
            transaction.RequestID = createResponce.RequestID;
            transaction.SubscriptionId = subscription.Id;

            transaction.TypeID = type;

            transaction.TelcoId = telcoid;

            return new TransactionsRepo().Insert(transaction);
        }

        public Result InsertMT(Subscription subscription, string message, bool Mtresult)
        {
            MT mT = new MT();
            mT.message = message;
            mT.MessagePartsCount = 1;
            mT.MtPurpose = "Welcome";
            mT.Price = 0;
            mT.Status = Mtresult ? "Success" : "faile";
            mT.SubscriberId = subscription.Id;
            return new MTRepo().Insert(mT);
        }
    }
}