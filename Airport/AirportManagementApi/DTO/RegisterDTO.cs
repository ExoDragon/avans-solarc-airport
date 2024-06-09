using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirportManagementApi.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        public string Claim { get; set; }
    }
}
