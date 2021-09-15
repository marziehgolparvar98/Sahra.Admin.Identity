using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sahra.DataLayer.Models.Entities
{
    public class NewsCategory : Category
    {
        [JsonIgnore]
        public virtual ICollection<News> News { get; set; }
    }
}
