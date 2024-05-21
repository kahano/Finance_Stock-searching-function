using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedDate{ get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }

        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }


        
    }
}