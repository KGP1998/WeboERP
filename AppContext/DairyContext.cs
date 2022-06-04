using DairyWeb.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DairyWeb.AppContext
{
    public class DairyContext : DbContext
    {

       
        public DbSet<RouteMaster> RouteMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderMaster> OrderMaster { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Distributor> Distributor { get; set; }
        public DbSet<Retailer> Retailer { get; set; }
        public DbSet<RetailerDetails> RetailerDetails { get; set; }
        public DbSet<TransactionMaster> TransactionMaster { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DairyContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

