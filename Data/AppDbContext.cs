
using Microsoft.EntityFrameworkCore;
using ConstructionRegistry.Models;

namespace ConstructionRegistry.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() : base() { }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Kontragent> Kontragents { get; set; }
        public DbSet<KadastrID> KadastrIDs { get; set; }
        public DbSet<TypeOfObject> TypeOfObjects { get; set; }
        public DbSet<ResponsiblPerson> ResponsiblPersons { get; set; }
        public DbSet<ContractCustomer> ContractCustomers { get; set; }
        public DbSet<ConstructionObject> ConstructionObjects { get; set; }
    }
}