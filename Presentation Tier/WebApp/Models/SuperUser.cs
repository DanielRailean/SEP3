using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SuperUser
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}