using Microsoft.EntityFrameworkCore;
using VenomHubPhotoWebSiteRestAPI.Middlewares;
using VenomHubPhotoWebSiteRestAPI.Models;

namespace VenomHubPhotoWebSiteRestAPI.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<AdminModel> Admin { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<PhotoModel> Photos { get; set; }
        public DbSet<LoginDetailModel> LoginDetails { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<PhotoModel>()
        //            .HasOne(photo => photo.Category)
        //            .WithMany(category => category.Photos)
        //            .HasForeignKey(photo => photo.CategoryId)
        //            .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<PhotoModel>()
        //        .HasOne(photo => photo.Album)
        //        .WithMany(album => album.Photos)
        //        .HasForeignKey(photo => photo.AlbumId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // Seed default admin user
        //    string defaultAdminId = Guid.NewGuid().ToString();
        //    string defaultAdminEmail = "admin@photo.com";
        //    string defaultAdminPassword = SimpleEncryptionHelper.Encrypt("Admin@123"); // Encrypt the default password

        //    modelBuilder.Entity<AdminModel>().HasData(
        //        new AdminModel
        //        {
        //            Id = defaultAdminId,
        //            AdminName = "Default Admin",
        //            AdminEmail = defaultAdminEmail,
        //            AdminPassword = defaultAdminPassword
        //        });
        //}
    }
}
