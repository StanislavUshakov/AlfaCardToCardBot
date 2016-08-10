using AlfaCardToCardBot.Validators;
using Microsoft.Bot.Builder.FormFlow;
using System.Diagnostics;
using AlfaCardToCardBot.Services;

#pragma warning disable 1998
namespace AlfaCardToCardBot
{
    public static class CardToCardFormBuilder
    {
        public static IForm<CardToCardTransfer> MakeForm()
        {
            FormBuilder<CardToCardTransfer> _order = new FormBuilder<CardToCardTransfer>();

            ValidateAsyncDelegate<CardToCardTransfer> validateCard =
                async (state, value) =>
                {
                    var cardNumber = value as string;
                    string errorMessage;

                    ValidateResult result = new ValidateResult();
                    result.IsValid = CardValidator.IsCardValid(cardNumber, out errorMessage);
                    result.Feedback = errorMessage;

                    return result;
                };

            return _order
                .Message("Добро пожаловать в сервис перевода денег с карты на карту!")
                .Field(nameof(CardToCardTransfer.SourceCardNumber), null, validateCard)
                .Field(nameof(CardToCardTransfer.Fee), state => false)
                .Field(nameof(CardToCardTransfer.ValidThruMonth))
                .Field(nameof(CardToCardTransfer.ValidThruYear))
                .Field(nameof(CardToCardTransfer.DestinationCardNumber), null, validateCard)
                .Field(nameof(CardToCardTransfer.CVV))
                .Field(nameof(CardToCardTransfer.Amount), null,
                async (state, value) =>
                {
                    int amount = int.Parse(value.ToString());

                    var alfabankService = new AlfabankService();                    
                    string auth = await alfabankService.AuthorizePartner();
                    state.Fee = (double) await alfabankService.GetCommission(auth, state.SourceCardNumber, state.DestinationCardNumber, amount);                   

                    ValidateResult result = new ValidateResult();
                    result.IsValid = true;
                    return result;
                })
                .Confirm("Вы хотите перевести {Amount} рублей с карты {SourceCardNumber} на карту {DestinationCardNumber}? Комиссия составит {Fee} рублей. (y/n)")
                .OnCompletionAsync(async (context, cardTocardTransfer) =>
                {                    
                    Debug.WriteLine("{0}", cardTocardTransfer);
                })
                .Build();
        }
    }
}