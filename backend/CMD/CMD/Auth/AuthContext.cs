using Microsoft.AspNet.Identity.EntityFramework;

namespace CMD.Auth
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("CMDContext") { }
    }
}