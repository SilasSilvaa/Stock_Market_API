using System.ComponentModel.DataAnnotations;


namespace api.Service
{
    public class Login
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}