using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    [Table("StockPortifolios")]
    public class StockPortifolio
    {
        public string? AppUserId { get; set; }
        public int StockId { get; set; }
        public StockDB? Stock { get; set; }
        public AppUser? AppUser { get; set; }
    }
}