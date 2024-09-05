using FJCO20240905.API.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace FJCO20240905.API.Models.DAL
{
    public class FJCOContext : DbContext
    {
        public FJCOContext(DbContextOptions<FJCOContext> options) : base(options)
        {
        }

        public DbSet<ProductFJCO> ProductsFJCO { get; set; }
    }
   
}
