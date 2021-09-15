using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum EnumRequestStatus
    {
        UnKnown = 0,

        [Description("ارسال شده ")]

        Submited = 1,

        [Description(" رد شده ")]

        Rejected = 2,

        [Description(" تایید شده")]

        Approved = 3,



    }
}
