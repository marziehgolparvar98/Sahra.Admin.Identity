using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class RelationOfInvestReq
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int InvestRequestId { get; set; }
        public InvestRequest InvestRequest { get; set; }
    }
}
