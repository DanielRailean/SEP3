using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class User
    {
        public long id { get; set; }
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } 
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [Required]
        public int PostalCode { get; set; }
        public int SecurityLevel { get; set; }
    }
}