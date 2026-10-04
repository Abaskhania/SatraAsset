using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SatraAsset.Model;


namespace SatraAsset.Data
{
    public class ApplicationDBContext :DbContext
    {

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ServiceLocation> ServiceLocations { get; set; }
        public DbSet<SatraUser> SatraUser { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }
        public DbSet<AssetProperty> AssetProperties { get; set; }
        public DbSet<AssetPropertyValue> AssetPropertyValues { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }
    }
}
