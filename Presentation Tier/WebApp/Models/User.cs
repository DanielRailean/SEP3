using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PostalCode { get; set; }
        public int SecurityLevel { get; set; }

        public void Update(User user)
        {
            UserId = user.UserId;
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