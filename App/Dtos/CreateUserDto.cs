using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(16)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}