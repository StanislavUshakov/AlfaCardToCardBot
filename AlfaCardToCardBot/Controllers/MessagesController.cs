using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using AlfaCardToCardBot.Services;

namespace AlfaCardToCardBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<CardToCardTransfer> MakeRoot()
        {
            return Chain.From(() => FormDialog.FromForm(CardToCardFormBuilder.MakeForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;

                        var alfaService = new AlfabankService();
                        string expDate = completed.ValidThruYear.ToString() + ((int)completed.ValidThruMonth).ToString("D2");
                        string confirmationUrl = await alfaService.TransferMoney(completed.SourceCardNumber, expDate, completed.CVV, completed.DestinationCardNumber, completed.Amount);

                        await context.PostAsync($"Осталось только подтвердить платеж. Перейдите по адресу https://testjmb.alfabank.ru/uapidemo/uapi/v1/check3ds.html?md=42d5807cbd464e3692677e62c528ee73");
                    }
                    catch (FormCanceledException<CardToCardTransfer> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = $"Вы прервали операцию, попробуем позже!";
                        }
                        else
                        {
                            reply = "Извините, произошла ошибка. Попробуйте позже.";
                        }
                        await context.PostAsync(reply);
                    }
                });
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            //var service = new AlfaCardToCardBot.Services.AlfabankService();
            //var output = await service.AuthorizePartner("TEST", "test_user_secret");
            //var fee = service.GetCommission(output, "5234567890123456", "4234567890123456", 100);

            if (message.Type == "Message")
            {
                return await Conversation.SendAsync(message, MakeRoot);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                return message.CreateReplyMessage("Вас приветствует Бот Alfa CardToCard! Отправьте любое сообщение для начала работы.");
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}