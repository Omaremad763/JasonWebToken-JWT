using System.ComponentModel.DataAnnotations;

namespace jwt__dev_Creed.model
{
    public class TokenRequest_model
    {

        [Required]
        public string E_mail {get; set;}

        [Required]

        public string password { get; set; }
  
    
    }
}
