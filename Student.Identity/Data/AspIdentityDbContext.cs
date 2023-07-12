using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Student.Identity.Data
{
    public class AspIdentityDbContext : IdentityDbContext
    {
        public AspIdentityDbContext(DbContextOptions<AspIdentityDbContext> options) : base(options)
        {
        }

    }
}
