using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User : SuperUser
    {
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
    }
}