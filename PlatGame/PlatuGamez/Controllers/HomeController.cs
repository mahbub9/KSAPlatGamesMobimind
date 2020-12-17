using ForestInterActive;
using Newtonsoft.Json.Linq;
using PlatGames.BL;
using PlatGames.DAL;
using PlatGames.DAL.DataRepo;
using PlatGames.Models;
using PlatGames.PlatGames.DAL.DataRepo;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlatGame.Controllers
{
    public class HomeController : Controller
    {

        //private IHttpContextAccessor _accessor;
        //private ILandingBL _landingBl;
        //private ICampaignBL _campaign;
        //private IClientTokenBL _clientToken;
        //private IClientAccessTokenService _clientAccessTokenService;

        //public HomeController(/*IHttpContextAccessor accessor,*/ ILandingBL landingBl, ICampaignBL campaign)
        //{
        //    //_accessor = accessor;
        //    _landingBl = landingBl;
        //    _campaign = campaign;

        //}


        private string GetIPAddress()
        {
            try
            {
                IPHostEntry Host = default(IPHostEntry);
                string Hostname = null;
                Hostname = System.Environment.MachineName;
                Host = Dns.GetHostEntry(Hostname);
                string IpAddress = null;
                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IpAddress = Convert.ToString(IP);
                    }
                }
                return IpAddress;
            }
            catch
            {
                return "Failed to get IP";
            }
        }

        public ActionResult Index(string txid = null, string affid = null, string pubid = null, string pageid = null)
        {
            if (!string.IsNullOrEmpty(txid) && !string.IsNullOrEmpty(affid) &&
                !string.IsNullOrEmpty(pubid) && !string.IsNullOrEmpty(pageid))
            {
                return RedirectToAction("Subscribe", new { txid = txid, affid = affid, pubid = pubid, pageid = pageid });
            }

            GameCentral GC = new GameCentral();
            var games = GC.GetGames(4004, null, null, 7);
            var data = GC.GetCategories(4004, null, 7);
            return View(games);


        }

        public ActionResult Subscribe()
        {
            return View();
        }

        public ActionResult Mobily(string txid, string affid, string pubid, string pageid)
        {
            //http://galaxylp.mobi-mind.net/?Id=1317,b1,966,2695,860,,0,42003,12030
            string[] MMParam = new string[] { "1317", "b1", "966", "2695", "860", "0", "42003", "12030" };
            string RedirectUrl = ProccessLanding(txid, affid, pubid, pageid, MMParam);

            return Redirect(RedirectUrl);
        }
        public ActionResult StcDaily(string txid, string affid, string pubid, string pageid)
        {
            //http://galaxylp.mobi-mind.net/?Id=1317,b1,966,2695,860,,0,42001,12028
            string[] MMParam = new string[] { "1317", "b1", "966", "2695", "860", "0", "42001", "12028" };
            string RedirectUrl = ProccessLanding(txid, affid, pubid, pageid, MMParam);

            return Redirect(RedirectUrl);
        }

        public ActionResult StcMonthly(string txid, string affid, string pubid, string pageid)
        {
            //http://galaxylp.mobi-mind.net/?Id=1317,b1,966,2695,860,,0,42001,12029
            string[] MMParam = new string[] { "1317", "b1", "966", "2695", "860", "0", "42001", "12029" };
            string RedirectUrl = ProccessLanding(txid, affid, pubid, pageid, MMParam);

            return Redirect(RedirectUrl);
        }

        public string ProccessLanding(string txid, string affid, string pubid, string pageid, string[] MMParam)
        {
            //var Ip = GetIPAddress();//_accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var callbackUrl = "http://ksa.platgames.co/home/Success";
            var UrlEncodeing = HttpUtility.UrlEncode(callbackUrl);
            string RedirectUrl = $"http://galaxylp.mobi-mind.net/?" +
                $"Id={MMParam[0]},{MMParam[1]},{MMParam[2]},{MMParam[3]},{MMParam[4]}," +
                $"{UrlEncodeing},{MMParam[5]},{MMParam[6]},{MMParam[7]}";

            string Lsource = "portal";
            string AppendCMInfo = $"&Ldate={DateTime.Now}&txid={txid}&affid={affid}&pubid={pubid}&pageid={pageid}";
            if (!string.IsNullOrEmpty(txid) && !string.IsNullOrEmpty(affid) &&
               !string.IsNullOrEmpty(pubid) && !string.IsNullOrEmpty(pageid))
            {
                Lsource = "campaign";
                AppendCMInfo = $"&Lsource={Lsource}" + AppendCMInfo;
                Task.Factory.StartNew(() => { new AddLandingRepo().AddLanding(); });
                //Task.Factory.StartNew(() => { AddClickTOCM(AppendCMInfo); });
            }
            AppendCMInfo = $"&Lsource={Lsource}" + AppendCMInfo;
            Logs.Log(RedirectUrl + AppendCMInfo, "Landing");

            return RedirectUrl + AppendCMInfo;
        }

        public ActionResult GameDetail(string gameId, string categoryName)
        {
            //ViewBag.categoryName = categoryName;
            //GameCentral GC = new GameCentral();
            //var game = GC.GetById(gameId, null);
            //ViewBag.categoryId = game.CategoryID;
            //ViewBag.gamesByCategory = GC.GetGames(4004, categoryName, null, 7, 0, 4);
            //return View(game);
            return RedirectToAction("PlayGame", new { gameId = gameId });
        }

        public ActionResult PlayGame(string gameId)
        {
            //bool IsSubscribed = false;

            //if (IsSubscribed)
            //{

            //}

            return RedirectToAction("GameView", new { id = gameId });
        }

        [Authorize]
        public ActionResult GameView(string id)
        {
            GameCentral GC = new GameCentral();
            var game = GC.GetById(id, null);
            if (game == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.gameurl = game.GameAddress;
            return View();
        }

        public ActionResult AllGames()
        {
            GameCentral GC = new GameCentral();
            //var category = GC.GetCategories(4004, null, 7);
            ViewBag.Category = GC.GetCategories(4004, null, 7);
            var games = GC.GetGames(4004, null, null, 7);
            return View(games);
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult TnC()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Success(string CGMSISDN, string CGOperatorID, string CGStatus)
        {
            if (CGStatus == "0" || CGStatus == "5")
            {
                //Login
                FormsAuthentication.SetAuthCookie(CGMSISDN, true);
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult UnSubscribe()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            System.Web.HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string msisdn = ticket.Name; // UserName (in this case msisdn)

            var subscription = new SubscriptionRepo().FindBy(x => x.Msisdn == msisdn).FirstOrDefault();

            if (subscription.IsSubscribed)
            {
                CallBackModel callbackModel = new CallBackModel();
                //UnSubscribe

                Result subscrbtionresult = new SubscriptionLogic().UnSubscrbtion(callbackModel, subscription, "Portal", subscription.TelcoId);

                if (subscrbtionresult.State == ResultState.Success)
                {
                    TransactionType transactionType = new TransactionTypesRepo().FindBy(c => c.Code == "BLD-SB").FirstOrDefault();
                    new SubscriptionLogic().InsertSubscrbtionHistory(subscription);
                    Result deleteresult = new SubscriptionLogic().InsertTransaction(subscription, callbackModel, transactionType.ID, subscription.TelcoId);
                    if (deleteresult.State == ResultState.Success)
                    {
                        ForestInterActive.CampaignManager.CampaignUnsubscitpion(subscription.Txid, (deleteresult.Data as PlatGames.DAL.Transaction).Id.ToString());
                    }
                }

                //Unsubscribe
                //CallBackModel callbackModel = new CallBackModel();
                ////callbackModel.STATUS = "BLD-SB";
                //subscription.TerminationMethod = "BLD-SB";
                //Result subscrbtionresult = new SubscriptionLogic().UnSubscrbtion(callbackModel, subscription, "Portal", subscription.TelcoId);

                if (subscrbtionresult.State == ResultState.Success)
                {
                    //Logout from portal
                    FormsAuthentication.SignOut();

                    var transaction = new TransactionsRepo().FindBy(x => x.SubscriptionId == subscription.Id).FirstOrDefault();
                    var telco = new TelcoInfoRepo().FindBy(x => x.Id == subscription.TelcoId).FirstOrDefault();
                    // Call MobiMind API to UnSub
                    callbackModel.User = "5C12475E2D6841D7891BF5F6C38D90E2";
                    callbackModel.Password = "viicd6Q9";
                    callbackModel.ServiceID = 1317;
                    callbackModel.ChannelID = transaction.ChannelID;
                    string profileId = "1675";
                    callbackModel.OperatorID = Convert.ToInt32(telco.Code);
                    string request = "BLD-SB";
                    callbackModel.RequestID = "00001";

                    string redirectUrl = $"http://galaxyapi.mobi-mind.net/Galaxy.ashx?" +
                        $"User={callbackModel.User}&password={callbackModel.Password}&MSISDN={msisdn}" +
                        $"&ServiceID={callbackModel.ServiceID}&ChannelID={callbackModel.ChannelID}&ProfileID={profileId}" +
                        $"&OperatorID={callbackModel.OperatorID}&Request={request}&RequestID={callbackModel.RequestID}";

                    return Redirect(redirectUrl);
                }
                
            }

            return RedirectToAction("Index");
        }

        private bool IsSubscribed(string msisdn)
        {
            var subscriber = new SubscriptionRepo().FindBy(x => x.Msisdn == msisdn && x.IsSubscribed == true).FirstOrDefault();
            return subscriber != null ? true : false;
        }
    }
}