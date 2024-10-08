using System.ComponentModel.DataAnnotations;

namespace api.Service
{
    public class UserEmail
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}