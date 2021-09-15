using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum EnumPlatform /*:EnumBase*/
    {
        UnKnown = 0,

        [Description("اندروید")]

        Android = 1,

        [Description("ios")]

        ios = 2,

        [Description("وب سایت ")]

        Other = 3
    }
}
