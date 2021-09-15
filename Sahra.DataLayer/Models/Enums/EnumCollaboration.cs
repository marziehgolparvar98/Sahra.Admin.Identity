using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum EnumCollaboration
    {
        UnKnown = 0,

        [Description("فضای کار")]

        WorkSpace = 1,

        [Description("سرمایه گذاری")]

        Investment = 2,

        [Description("منتینگ و مشاوره")]

        MentoringAndConsulting = 3,

        [Description("بازاریابی و تجاری سازی")]

        MarketingAndCommercialization = 4,
    }
}
