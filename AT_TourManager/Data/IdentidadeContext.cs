using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AT_TourManager.Data
{
    public class IdentidadeContext : IdentityDbContext
    {
        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options)
        {
        }
    }
}
