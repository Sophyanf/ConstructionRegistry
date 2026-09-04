using ConstructionRegistry.Models;
using Microsoft.EntityFrameworkCore;
namespace ConstructionRegistry.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConstructionRegistryDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConstructionObject>()
                .HasOne(co => co.ConstructionOrganization)
                .WithMany()   // без обратной коллекции
                .HasForeignKey(co => co.ConstructionOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConstructionObject>()
                .HasOne(co => co.Customer)
                .WithMany()
                .HasForeignKey(co => co.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ConstructionObject>()
                .HasOne(co => co.ConstructionOrganizationSub)
                .WithMany()
                .HasForeignKey(co => co.ConstructionOrganizationSubId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Kontragent> Kontragents { get; set; }
        public DbSet<KadastrID> KadastrIDs { get; set; }
        public DbSet<TypeOfObject> TypeOfObjects { get; set; }
        public DbSet<ResponsiblPerson> ResponsiblPersons { get; set; }
        public DbSet<ContractCustomer> ContractCustomers { get; set; }
        public DbSet<ConstructionObject> ConstructionObjects { get; set; }
    }
}