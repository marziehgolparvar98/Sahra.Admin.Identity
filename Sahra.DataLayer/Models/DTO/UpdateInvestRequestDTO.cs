using Sahra.DataLayer.Models.Enums;

namespace Sahra.DataLayer.Models.DTO
{
    public class ShowUpdateInvestRequestViewModel
    {
        public int Id { get; set; }
        public EnumRequestStatus EnumRequestStatus { get; set; }

    }
}
