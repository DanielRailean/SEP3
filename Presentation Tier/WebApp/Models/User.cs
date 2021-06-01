using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class User
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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
        [JsonPropertyName("phone")]

        public int Phone { get; set; }
        [Required]
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [Required]
        [JsonPropertyName("postalCode")]

        public int PostalCode { get; set; }
        
        [Required]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [Required]
        [JsonPropertyName("securityLevel")]

        public int SecurityLevel { get; set; }
    }
}