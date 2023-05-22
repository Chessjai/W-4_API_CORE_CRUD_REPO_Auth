using Microsoft.EntityFrameworkCore;

namespace W_4_API_CORE_CRUD_REPO
{
    public class CoreDbcontext:DbContext
    {
        public CoreDbcontext(DbContextOptions<CoreDbcontext> options)

         : base(options)

        {



            // To ensure that database is created through dbcontext

        }

        public DbSet<Product> products { get; set; }
    }
}
