using System.Collections.Generic;

namespace jwt__dev_Creed.model
{
    public class Auth_model
    {
    
        public string message { get; set; }

        public bool is_auth { get; set; }

        public string user_name { get; set; }


        public string E_mail { get; set; }

        public List <string>  roles{ get; set; }

        public string token { get; set; }


        public DateTime Expiration_date { get; set; }


    }
}
