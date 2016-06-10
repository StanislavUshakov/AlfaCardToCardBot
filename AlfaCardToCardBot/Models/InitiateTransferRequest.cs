namespace AlfaCardToCardBot.Models
{
    public class SenderCard
    {
        public string number { get; set; }
        public string exp_date { get; set; }
        public string cvv { get; set; }
    }

    public class Sender
    {
        public SenderCard card { get; set; }        
    }

    public class Recipient
    {
        public Card card { get; set; }
    }

    public class InitiateTransferRequest
    {
        public Sender sender { get; set; }
        public Recipient recipient { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}