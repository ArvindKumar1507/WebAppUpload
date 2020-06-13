using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApp.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int PhoneNumber { get; set; }
    }
}
