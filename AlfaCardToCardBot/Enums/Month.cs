using Microsoft.Bot.Builder.FormFlow;

namespace AlfaCardToCardBot.Enums
{
    public enum Month
    {
        [Terms(new string[] { "Jan", "Январь", "янв" })]
        Январь = 1,

        [Terms(new string[] { "Feb", "Февраль", "фев" })]
        Февраль = 2,

        [Terms(new string[] { "Mar", "Март", "мар" })]
        Март = 3,

        [Terms(new string[] { "Apr", "Апрель", "апр" })]
        Апрель = 4,

        [Terms(new string[] { "May", "Май", "май" })]
        Май = 5,

        [Terms(new string[] { "Jun", "Июнь", "июн" })]
        Июнь = 6,

        [Terms(new string[] { "Jul", "Июль", "июл" })]
        Июль = 7,

        [Terms(new string[] { "Aug", "Август", "авг" })]
        Август = 8,

        [Terms(new string[] { "Sep", "Сентябрь", "сен" })]
        Сентябрь = 9,

        [Terms(new string[] { "Oct", "Октябрь", "окт" })]
        Октябрь = 10,

        [Terms(new string[] { "Nov", "Ноябрь", "ноя" })]
        Ноябрь = 11,

        [Terms(new string[] { "Dec", "Декабрь", "ltr" })]
        Декабрь = 12
    }
}