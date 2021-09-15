using System.ComponentModel;

namespace Sahra.DataLayer.Models.Enums
{
    public enum ProductType
    {
        Unknown = 0,
        [Description("نوع یک")]
        TypeOne = 1,
        [Description("نوع دو")]
        TypeTwo = 2,
        [Description(" نوع سه ")]
        TypeThree = 3,
    }
}
