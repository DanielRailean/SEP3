using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [JsonPropertyName("phone")]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [JsonPropertyName("postalCode")]
        public int PostalCode { get; set; }
        [Required]
        [JsonPropertyName("securityLevel")]
        public int SecurityLevel { get; set; }

        public void Update(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone = user.Phone;
            Address = user.Address;
            PostalCode = user.PostalCode;
            SecurityLevel = user.SecurityLevel;
        }
    }
}