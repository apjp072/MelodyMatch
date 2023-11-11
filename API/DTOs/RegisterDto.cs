using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } //gets sent in json as username and password (lower case)
        
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}


