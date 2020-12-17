using ForestInterActive;
using Newtonsoft.Json.Linq;
using PlatGame.Service.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Service.Models
{
    public class ClientAccessToken : IClientAccessTokenService
    {     

        public TokenModel GenerateToken()
        {
            TokenModel tokenModel = null;
            try
            {
                var client = new RestClient("https://staging-gateway.mondiapay.com/v1/api/oauth/token?grant_type=client_credentials&client_id=f73be2a9-92e7-4e32-aa3f-344175d07d7e&client_secret=3cca7936-85d0-4a49-8f2f-579051a88afe");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    tokenModel = new TokenModel();
                    var jObject = JObject.Parse(response.Content);
                    tokenModel.AccessToken = jObject.GetValue("access_token").ToString();
                    tokenModel.LifespanInSec = Convert.ToInt32(jObject.GetValue("expires_in"));
                }

                else
                {
                    Logs.Log("Request to Mondia for access token failed.", "GetToken");
                    Logs.Log(response.Content.ToString(), "GetToken");
                }
            }

            catch (Exception ex)
            {
                Logs.Log(ex);
            }

            return tokenModel;
        }
    }
}
