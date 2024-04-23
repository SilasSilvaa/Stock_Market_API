using System.ComponentModel.DataAnnotations.Schema;

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