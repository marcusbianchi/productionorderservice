using Microsoft.EntityFrameworkCore;
using productionorderservice.Model;


namespace productionorderservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<PhaseParameter> PhaseParameters { get; set; }
        public DbSet<PhaseProduct> PhaseProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThingGroup> ThingGroups { get; set; }
        public DbSet<ProductionOrderType> ProductionOrderTypes { get; set; }
        public DbSet<AdditionalInformation> AdditionalInformations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<StateConfiguration> StateConfigurations { get; set; }
        public DbSet<ConfiguredState> ConfiguredStates { get; set; }
        public DbSet<HistState> HistStates{get;set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductionOrder>()
                .HasIndex(b => b.productionOrderNumber);
            modelBuilder.Entity<ProductionOrder>()
            .Property(b => b.currentStatus)
            .HasDefaultValue("created");            
        }
    }
}