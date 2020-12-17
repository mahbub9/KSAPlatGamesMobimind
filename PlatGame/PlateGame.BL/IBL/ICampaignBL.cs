using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface ICampaignBL
    {
        void AddClick(string param);
        void FireBack(string msisdn, string keyword, string txid, int CMTelcoID);
        void InsertCampaignDN(string transactionId, string msisdn, string MTsourceID, decimal price, int DNStatusID);
        void InsertCampaignMO(string transactionId, string msisdn);
        void InsertCampaignMT(string transactionId, string msisdn, decimal price, bool IsFirst);
        void InsertCampaignRenewal(string transactionId, string msisdn, decimal price);
        void InsertCampaignSubscribtion(string transactionID, string msisdn, decimal price, bool isfirst);
        void InsertCampaignUnSubscribtion(string msisdn, string transactionId);
    }
}
