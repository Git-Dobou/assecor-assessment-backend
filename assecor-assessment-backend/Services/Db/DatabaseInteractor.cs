using Microsoft.EntityFrameworkCore;

namespace web_api.Services.Db
{
    public class DatabaseInteractor : DbContext
    {
        public DatabaseInteractor(DbContextOptions<DatabaseInteractor> options)
        : base(options)
        {

        }

        public DatabaseInteractor()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Models.Zoo> Zoo { get; set; }
    }
}