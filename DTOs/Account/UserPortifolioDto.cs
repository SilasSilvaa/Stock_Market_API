using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Account
{
    public class UserPortifolioDto
    {
        [Required]
        public string Email { get; set; }
    }
}