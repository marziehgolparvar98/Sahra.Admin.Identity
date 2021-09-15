using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum EnumCategory
    {
        UnKown = 0,

        [Description("بورس")]

        Exchange = 1,

        [Description("بانک")]

        Bank = 2,

        [Description("بیمه")]

        Insurance = 3,


    }
}
