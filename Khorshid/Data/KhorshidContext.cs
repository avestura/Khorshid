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
        }

        public DbSet<TownData> TownData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
