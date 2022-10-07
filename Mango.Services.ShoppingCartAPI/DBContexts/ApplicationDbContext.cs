using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.DBContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
