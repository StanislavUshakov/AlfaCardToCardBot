using AlfaCardToCardBot.Enums;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace AlfaCardToCardBot
{
    [Serializable]
    public class CardToCardTransfer
    {
        [Prompt("Номер карты отправителя:")]
        [Describe("Номер карты, с которой Вы хотите перевести деньги")]
        public string SourceCardNumber;

        [Prompt("Номер карты получателя:")]
        [Describe("Номер карты, на которую Вы хотите перевести деньги")]
        public string DestinationCardNumber;

        [Prompt("VALID THRU (месяц):")]
        [Describe("VALID THRU (месяц)")]
        public Month ValidThruMonth;

        [Prompt("VALID THRU (год):")]
        [Describe("VALID THRU (год)")]
        [Numeric(2016, 2050)]
        public int ValidThruYear;

        [Prompt("CVV:")]
        [Describe("CVV (три цифры на обороте карточки)")]
        public string CVV;

        [Prompt("Сумма перевода (руб):")]
        [Describe("Сумма перевода (руб)")]
        public int Amount;

        [Prompt("Комиссия (руб):")]
        [Describe("Комиссия (руб)")]
        public double Fee;
    }
}