using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum JobType
    {
        Unkown = 0,
        [Description("backend")]
        Backend = 1,
        [Description("frontend")]
        Frontend = 2,
        [Description("Ux")]
        UX = 3,
        [Description("تحلیلگر کسب و کار")]
        BusinessAnalyst = 3,
        [Description("مدیر محصول")]
        ProductManager = 4,
    }
}
