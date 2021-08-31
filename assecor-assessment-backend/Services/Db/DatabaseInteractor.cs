using Microsoft.EntityFrameworkCore;

namespace assecor_assessment_backend.Services.Db
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

        public DbSet<Models.Person> Person { get; set; }
    }
}