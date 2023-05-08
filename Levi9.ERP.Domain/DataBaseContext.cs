using Levi9.ERP.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace Levi9.ERP.Domain
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Price>()
            .HasKey(e => new { e.ProductId, e.PriceListId });

            modelBuilder.Entity<Price>()
                .HasOne(e => e.Product)
                .WithMany(e => e.Prices)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Price>()
                .HasOne(e => e.PriceList)
                .WithMany(e => e.Prices)
                .HasForeignKey(e => e.PriceListId);

            modelBuilder.Entity<ProductDocument>()
            .HasKey(e => new { e.ProductId, e.DocumentId });

            modelBuilder.Entity<ProductDocument>()
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductDocuments)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<ProductDocument>()
                .HasOne(e => e.Document)
                .WithMany(e => e.ProductDocuments)
                .HasForeignKey(e => e.DocumentId);

            modelBuilder.Entity<Client>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}