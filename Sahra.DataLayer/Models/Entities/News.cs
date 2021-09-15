using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sahra.DataLayer.Models.Entities
{
    public class News : BaseProp
    {
        [Required(ErrorMessage = "درج توضیحات خبر اجباری است")]
        public string NewsDescription { get; set; }
        [Required(ErrorMessage = "درج درج کننده ی خبر اجباری است")]
        public string NewsCreator { get; set; }
        public int NewsCategoryId { get; set; }
        public string NewsSummary { get; set; }
        public DateTime CreatNews { get; set; }
        [JsonIgnore]
        public virtual NewsCategory NewsCategory { get; set; }

    }
}
