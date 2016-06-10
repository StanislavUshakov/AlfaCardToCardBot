using AlfaCardToCardBot.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlfaCardToCardBot.Services
{
    public class AlfabankService
    {
        public async Task<string> AuthorizePartner()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];

            HttpClient client = new HttpClient();

            var header = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + header);

            var result = await client.PostAsync(ConfigurationManager.AppSettings["AlfaUrl"] + "api/oauth/token?grant_type=client_credentials&scope=read", null);
            var stringContent = await result.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<AccessToken>(stringContent);
            return token.Token;
        }

        public async Task<decimal> GetCommission(string auth, string senderCard, string recipientCard, decimal amount)
        {
            HttpClient client = new HttpClient(new Helpers.LoggingHandler(new HttpClientHandler()));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);

            var request = new FeeRequest
            {
                sender = new Account
                {
                    card = new Card
                    {
                        number = senderCard
                    }
                },
                recipient = new Account
                {
                    card = new Card
                    {
                        number = recipientCard
                    }
                },
                amount = amount,
                currency = "RUR"
            };

            var result = await client.PostAsJsonAsync(ConfigurationManager.AppSettings["AlfaUrl"] + "uapi/v1/fee", request);

            var stringContent = await result.Content.ReadAsStringAsync();

            var feeReponse = JsonConvert.DeserializeObject<FeeResponse>(stringContent);
            return 90;
        }

        public async Task<string> TransferMoney(string senderCard, string expDate, string cvv, string recipientCard, decimal amount)
        {
            HttpClient client = new HttpClient(new Helpers.LoggingHandler(new HttpClientHandler()));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (await AuthorizePartner()));

            var request = new InitiateTransferRequest
            {
                sender = new Sender
                {
                    card = new SenderCard
                    {
                        number = senderCard,
                        cvv = cvv,
                        exp_date = expDate
                    },
                },
                recipient = new Recipient
                {
                    card = new Card
                    {
                        number = recipientCard
                    }
                },
                amount = amount,
                currency = "RUR"
            };

            try
            {
                var result = await client.PutAsJsonAsync(ConfigurationManager.AppSettings["AlfaUrl"] + "api/v1/transfers", request);

                string stringContent = await result.Content.ReadAsStringAsync();
                var transferResponse = JsonConvert.DeserializeObject<InitiateTransferResponse>(stringContent);

                var pareq = transferResponse.pareq.Replace("\\\n", "");
                    
                return transferResponse.acsURL;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}