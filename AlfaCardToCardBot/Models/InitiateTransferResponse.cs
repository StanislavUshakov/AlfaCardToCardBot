namespace AlfaCardToCardBot.Models
{
    public class TransferCard
    {
        public string id { get; set; }
    }

    public class TransferAccount
    {
        public TransferCard card { get; set; }
    }

    public class InitiateTransferResponse
    {
        public TransferAccount recipient { get; set; }
        public TransferAccount sender { get; set; }
        public string termURL { get; set; }
        public string status { get; set; }
        public string pareq { get; set; }
        public string md { get; set; }
        public string acsURL { get; set; }
        public string transaction_id { get; set; }
        public string template_id { get; set; }
    }
}