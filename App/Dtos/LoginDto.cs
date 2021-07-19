using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class LoginDto
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(16)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}