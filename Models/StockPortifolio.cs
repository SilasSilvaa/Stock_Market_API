using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    public class StockPortifolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public AppUser AppUser { get; set; }
    }
}