using Microsoft.EntityFrameworkCore;
using webeczane.DbTables;

namespace webeczane.EczaneContext
{
    public class DbEczaneContext : DbContext
    {
        public DbEczaneContext()
        {

        }
        public DbEczaneContext(DbContextOptions<DbEczaneContext> options) : base(options) { }

        public DbSet<Hastane> Hastane { get; set; }
    }
}