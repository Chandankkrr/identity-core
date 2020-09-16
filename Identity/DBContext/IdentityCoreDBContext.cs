using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DBContext
{
    public class IdentityCoreDbContext : IdentityDbContext
    {
        public IdentityCoreDbContext(DbContextOptions<IdentityCoreDbContext> options)
        : base(options)
        {
            
        }
    }
}