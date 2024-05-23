using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        [JsonIgnore]
         [IgnoreDataMember]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedDate{ get; set; } = DateTime.Now;

        
        [JsonIgnore]
         [IgnoreDataMember]
        public int? StockId { get; set; }

        
        [JsonIgnore]
         [IgnoreDataMember]
        public Stock? Stock { get; set; }

        
        [JsonIgnore]
         [IgnoreDataMember]
        public string AppUserId { get; set; }

        
        [JsonIgnore]
         [IgnoreDataMember]
        public AppUser appUser { get; set; }


        
    }
}