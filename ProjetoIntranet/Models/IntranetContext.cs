using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjetoIntranet.Models
{
    public class IntranetContext : DbContext
    {
        public IntranetContext()
            : base("IntranetContext")
        {
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Departament> Departament { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Database.SetInitializer<IntranetContext>(null);

            modelBuilder.Entity<Departament>()
                   .HasMany<News>(s => s.News)
                   .WithMany(c => c.Departaments)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("DepartamentRefID");
                       cs.MapRightKey("NewsRefID");
                       cs.ToTable("DepartamentNews");
                   });
        }
    }
}