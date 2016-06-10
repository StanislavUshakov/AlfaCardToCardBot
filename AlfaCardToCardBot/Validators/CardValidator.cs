using System.Linq;

namespace AlfaCardToCardBot.Validators
{
    public static class CardValidator
    {
        public static bool IsCardValid(string cardNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (cardNumber.Length != 16 || cardNumber.Any(ch => ch < '0' || ch > '9'))
            {
                errorMessage = "Номер карты должен состоять из 16 цифр.";
                return false;
            }

            return true;
        }
    }
}