using System.ComponentModel.DataAnnotations;

namespace api.Service
{
    public class User
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}