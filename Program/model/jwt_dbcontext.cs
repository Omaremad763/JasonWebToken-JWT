using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwt__dev_Creed.model
{
    public class jwt_dbcontext : IdentityDbContext<Application_user>

    {

        public jwt_dbcontext(DbContextOptions<jwt_dbcontext> options) : base(options)
        {

        }


    }
}
