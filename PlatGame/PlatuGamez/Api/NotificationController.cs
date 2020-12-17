using PlatGames.BL;
using ForestInterActive;
using PlatGames.DAL;
using PlatGames.DAL.DataRepo;
using PlatGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlatGame.Api
{
    public class NotificationController:ApiController
    {
        public static readonly string UserName = "5C12475E2D6841D7891BF5F6C38D90E2";
        public static readonly string Password = "viicd6Q9";
        [HttpGet]
        public string CallBack([FromUri] CallBackModel requestModel)
        {
            Logs.Log(HttpContext.Current.Request.RawUrl, "CallBack");
            try
            {

                if (requestModel.CGStatus != 0 && requestModel.CGStatus != -1)
                    return "OK";

                if (requestModel.User != UserName || requestModel.Password != Password)
                   // throw new ArgumentException("Username or password invalid");

                if (!ModelState.IsValid)
                {
                    return "Missing Parameter";
                }

                
                TransactionType transactionType = new TransactionTypesRepo().FindBy(c=>c.Code== requestModel.STATUS).FirstOrDefault();

                SubscriptionRepo subscriptionRepo = new SubscriptionRepo();
                LandingClick landingClick = null;
                SubscriptionLogic subscriptionLogic = new SubscriptionLogic();

                if (transactionType == null)
                {
                    Logs.Log(HttpContext.Current.Request.RawUrl, "UnknownStatus");
                    return "OK";
                }
                Subscription subscription = subscriptionRepo.FindBy(c => c.Msisdn == requestModel.MSISDN).FirstOrDefault();
                TelcoInfo telco = new TelcoInfoRepo().FindBy(c => c.Code == requestModel.OperatorID).FirstOrDefault();
                int telcoid = 1;
                if (telco != null)
                {
                    telcoid = telco.Id;
                }
                switch (requestModel.STATUS)
                {
                    case "ACT-SB"://subscribe only
                    case "FSC-BL"://first billing
                       
                        bool isfirst = false;
                        if (subscription == null)
                        {
                            subscription = new Subscription();
                            isfirst = true;
                        }
                        if (!string.IsNullOrEmpty(requestModel.txid))
                        {
                            landingClick  = subscriptionLogic.AddLanding(requestModel);
                        }
                        Result result = subscriptionLogic.InsertSubscrbtion(requestModel, subscription, telcoid);

                        if (result.State == ResultState.Success && requestModel.STATUS == "FSC-BL")
                        {
                            int renewal = (requestModel.ChannelID == 1209 || requestModel.ChannelID == 1230) ? 30 : 1;
                            subscriptionLogic.InsertSubscrbtionHistory(subscription);
                            Result transresult = new SubscriptionLogic().InsertTransaction(subscription, requestModel, transactionType.ID, telcoid);
                            //ForestInterActive.CampaignManager.ScrabberFireBack(subscription.Txid, subscription.Msisdn, "TTusr", telco.CMId.Value, "",7); check about this logic
                            ForestInterActive.CampaignManager.ScrabberFireBack(subscription.Txid, subscription.Msisdn, "TTusr", telco.CMId.Value, "", renewal);
                            ForestInterActive.CampaignManager.CampaignSubscitpion(subscription.Txid, (transresult.Data as PlatGames.DAL.Transaction).Id.ToString(), isfirst, 25);
                        }
                        else
                        {
                            Result transresult = new SubscriptionLogic().InsertTransaction(subscription, requestModel, transactionType.ID, telcoid);
                        }
                        return "OK";
                    case "BLD-SB"://unsub
                    case "RCL-SB"://recycle
                        if (subscription == null)
                        {
                            Logs.Log(HttpContext.Current.Request.RawUrl, "MsisdnNotfound");
                            return "OK";
                        }
                        Result subscrbtionresult = subscriptionLogic.UnSubscrbtion(requestModel, subscription, "API", telcoid);
                        if (subscrbtionresult.State == ResultState.Success)
                        {
                            new SubscriptionLogic().InsertSubscrbtionHistory(subscription);
                            Result deleteresult = new SubscriptionLogic().InsertTransaction(subscription, requestModel, transactionType.ID, telcoid);
                            if (deleteresult.State == ResultState.Success)
                            {
                                ForestInterActive.CampaignManager.CampaignUnsubscitpion(subscription.Txid, (deleteresult.Data as PlatGames.DAL.Transaction).Id.ToString());
                            }
                        }
                        return "OK";
                    case "FFL-BL"://filed to bill first time
                    case "RFL-BL"://filed renewal
                        if (subscription == null)
                        {
                            Logs.Log(HttpContext.Current.Request.RawUrl, "MsisdnNotfound");
                            return "OK";
                        }
                         subscrbtionresult = subscriptionLogic.UnSubscrbtion(requestModel, subscription, "API", telcoid);
                        if (subscrbtionresult.State == ResultState.Success)
                        {
                            new SubscriptionLogic().InsertSubscrbtionHistory(subscription);
                            Result deleteresult = new SubscriptionLogic().InsertTransaction(subscription, requestModel, transactionType.ID, telcoid);
                        }
                            return "OK";
                    case "RSC-BL":
                        Result renewalresult = new SubscriptionLogic().InsertTransaction(subscription, requestModel, transactionType.ID, telcoid);
                        if (renewalresult.State == ResultState.Success)
                        {
                            new SubscriptionLogic().ActiveSubscriber(subscription);
                            ForestInterActive.CampaignManager.CampaignRenewal(subscription.Txid, (renewalresult.Data as PlatGames.DAL.Transaction).Id.ToString(), requestModel.Price.HasValue?requestModel.Price.Value:0);
                        }
                        return "OK";
                    default:
                        Logs.Log(HttpContext.Current.Request.RawUrl, "UnknownStatus");
                        return "OK";
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex.ToString(), "CallBackError");
                return "An error occured, please contact Forest Interactive";
            }
        }
    }
}