using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
