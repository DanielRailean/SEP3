using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User
    {
        public int user_id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        // [Required, Range(6, 255, ErrorMessage = "Password must be at least 6 characters (max. 255)")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PostalCode { get; set; }
    }
}