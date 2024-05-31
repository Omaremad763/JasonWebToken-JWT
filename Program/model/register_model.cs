using System.ComponentModel.DataAnnotations;

namespace jwt__dev_Creed.model
{
    public class register_model
    {

        [Required]
        [MaxLength(5)]
        public string first_name { get; set; }

        [Required]
        [MaxLength(5)]
        public string last_name { get; set; }

        [Required]
        [MaxLength(10)]
        public string User_name { get; set; }

        [Required]
        [MaxLength(100)]
        public string E_mail { get; set; }

        [Required]
        [MaxLength(20),MinLength(8)]
        public string Password { get; set; }


    }
}
