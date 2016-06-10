namespace AlfaCardToCardBot.Models
{
    public class Card
    {
        public string number { get; set; }
    }

    public class Account
    {
        public Card card { get; set; }
    }

    public class FeeRequest
    {
        public Account sender { get; set; }

        public Account recipient { get; set; }

        public decimal amount { get; set; }

        public string currency { get; set; }
    }
}