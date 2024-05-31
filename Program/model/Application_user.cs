using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace jwt__dev_Creed.model
{
    public class Application_user :IdentityUser
    {

        [Required]
        [MaxLength(5)]
        public string first_name { get; set; }
        
        [Required]
        [MaxLength(5)]
        public string last_name { get; set; }

    }

}
