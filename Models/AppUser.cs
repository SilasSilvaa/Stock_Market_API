using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser
    {
        public List<StockPortfolio> Portfolio { get; set; } = new List<StockPortfolio>();
    }
}