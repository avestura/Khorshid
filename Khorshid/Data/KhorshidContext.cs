namespace Khorshid.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class KhorshidContext : DbContext
    {
        public KhorshidContext()
            : base("name=KhorshidContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<KhorshidContext>());
        }

        public DbSet<TownData> TownData { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<DriverWork> DriverWorks { get; set; }

        public DbSet<WorkPage> WorkPages { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
