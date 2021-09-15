using System;
using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
