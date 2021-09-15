using Sahra.DataLayer.Models.Enums;

namespace Sahra.DataLayer.Models.Entities
{
    public class CategoryInvestReq : RelationOfInvestReq
    {
        public EnumCategory EnumCategory { get; set; }
    }
}
