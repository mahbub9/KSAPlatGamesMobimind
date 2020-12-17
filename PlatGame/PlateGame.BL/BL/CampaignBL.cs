using ForestInterActive;
using PlateGame.BL.IBL;
using PlatGame.Data;
using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.BL
{
    public class CampaignBL : ICampaignBL
    {
        private ISubscriptionRepo _subscriptionRepo;
        private ICampaignRepo _campaignRepo;

        public CampaignBL(ISubscriptionRepo subscriptionRepo, ICampaignRepo campaignRepo)
        {
            _subscriptionRepo = subscriptionRepo;
            _campaignRepo = campaignRepo;
        }
        public void AddClick(string param)
        {
            string Url = "http://campaignmanager.fun.moobig.com/PagesCall?isVisit=true&isFirstclick=false&isSubClick=false&isFirstclick=false" + param;
            var info = HtmlCallHelper.HttpGetReq(Url, 350000);
        }

        public void FireBack(string msisdn, string keyword, string txid, int CMTelcoID)
        {
            string accesskey = "Musr0"; // need to change 
            string CampaignManagerInsertResult = CampaignManager.ScrabberFireBack(txid, msisdn, accesskey, CMTelcoID, "", 7);

            // insert fireback log
            FireBackLog log = new FireBackLog
            {
                CreatedDate = DateTime.Now,
                Msisdn = msisdn,
                Txid = txid,
                Result = CampaignManagerInsertResult
            };
            _campaignRepo.AddFireBackLog(log);

            Logs.Log(CampaignManagerInsertResult, "Campaign//FireBack");
        }

        public void InsertCampaignSubscribtion(string transactionID, string msisdn, decimal price, bool isfirst)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignSubscitpion(txid, transactionID, isfirst, price);

            Logs.Log(CampaignManagerInsertResult, "Campaign//Subscrption");
        }

        public void InsertCampaignDN(string transactionId, string msisdn, string MTsourceID, decimal price, int DNStatusID)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignDN(txid, transactionId, MTsourceID, DNStatusID, price);

            Logs.Log(CampaignManagerInsertResult, "Campaign//DN");
        }

        public void InsertCampaignMO(string transactionId, string msisdn)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignMO(txid, transactionId);

            Logs.Log(CampaignManagerInsertResult, "Campaign//MO");
        }

        public void InsertCampaignRenewal(string transactionId, string msisdn, decimal price)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignRenewal(txid, transactionId, price);

            Logs.Log(CampaignManagerInsertResult, "Campaign//Renewal");
        }

        public void InsertCampaignMT(string transactionId, string msisdn, decimal price, bool IsFirst)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignMT(txid, transactionId, 2, price, IsFirst);

            Logs.Log(CampaignManagerInsertResult, "Campaign//MT");
        }

        public void InsertCampaignUnSubscribtion(string msisdn, string transactionId)
        {
            var txid = "";
            var userInfo = _subscriptionRepo.GetSubscriperByMsisdn(msisdn);
            if (userInfo != null)
                txid = userInfo.txid;

            string CampaignManagerInsertResult = CampaignManager.CampaignUnsubscitpion(txid, transactionId);

            Logs.Log(CampaignManagerInsertResult, "Campaign//UnSubscription");
        }
    }
}
