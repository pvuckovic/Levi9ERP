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

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<PriceList>().HasData(
                 new PriceList
                 {
                     Id = 1,
                     GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db41a"),
                     Name = "USD Price List",
                     LastUpdate = "634792557112051692"
                 }
                );
            modelBuilder.Entity<Client>().HasData(
                 new Client
                 {
                     Id = 1,
                     GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db41b"),
                     Name = "Zlatko",
                     Address = "Njegoseva 2",
                     Email = "zlatko123@gmail.com",
                     Password = "test",
                     Salt = "test1",
                     Phone = "064322222",
                     LastUpdate = "634792557112051692",
                     PriceListId = 1
                 }
                );
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    Id = 1,
                    GlobalId = new Guid("494ad822-8ee2-47c3-938f-2de7a43db41a"),
                    LastUpdate = "634792557112051692",
                    DocumentType = "INVOICE",
                    ClientId = 1
                }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    GlobalId = new Guid("494ad824-8ea2-47c3-938f-2de7a43db41a"),
                    Name = "Shirt",
                    ImageUrl = "someurl123344444",
                    AvailableQuantity = 70,
                    LastUpdate = "634792557112051692",
                }
               );
            modelBuilder.Entity<Price>().HasData(
                new Price
                {
                    GlobalId = new Guid("494ad824-8ee2-47c3-932f-2de7a43db41a"),
                    PriceValue = 12,
                    Currency = "USD",
                    LastUpdate = "634792557112051692",
                    ProductId = 1,
                    PriceListId = 1,
                }
                );

            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 2,
                    GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a13db41a"),
                    Name = "EUR Price List",
                    LastUpdate = "634792557112051693"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 2,
                    GlobalId = new Guid("494ad824-8ef7-47c3-938f-2de7a43db41a"),
                    Name = "A T-Shirt",
                    ImageUrl = "slika.png",
                    AvailableQuantity = 140,
                    LastUpdate = "634792557112051693",
                }
                );
            modelBuilder.Entity<Price>().HasData(
                new Price
                {
                    GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de7a20db41a"),
                    PriceValue = 30,
                    Currency = "EUR",
                    LastUpdate = "634792557112051693",
                    ProductId = 2,
                    PriceListId = 2,
                }
               );

            modelBuilder.Entity<PriceList>().HasData(
                new PriceList
                {
                    Id = 3,
                    GlobalId = new Guid("494b7014-8ee2-47c3-938f-2de7a43db41a"),
                    Name = "RSD Price List",
                    LastUpdate = "634792557112051694"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 3,
                    GlobalId = new Guid("494ad824-8ee2-47c3-938f-2de71b3db41a"),
                    Name = "Z T-Shirt",
                    ImageUrl = "slika2.png",
                    AvailableQuantity = 150,
                    LastUpdate = "634792557112051694",
                }
                );
            modelBuilder.Entity<Price>().HasData(
                new Price
                {
                    GlobalId = new Guid("492ad82b-8ee2-47c3-938f-2de7a43db41a"),
                    PriceValue = 1500,
                    Currency = "RSD",
                    LastUpdate = "634792557112051694",
                    ProductId = 3,
                    PriceListId = 3,
                }
               );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 4,
                    GlobalId = new Guid("494ad824-8ee2-47c3-126f-2de7a43db41a"),
                    Name = "B T-Shirt",
                    ImageUrl = "slika3.png",
                    AvailableQuantity = 550,
                    LastUpdate = "634792557112051695",
                }
                );
            modelBuilder.Entity<Price>().HasData(
                new Price
                {
                    GlobalId = new Guid("494bc224-8ee2-47c3-938f-2de7a43db41a"),
                    PriceValue = 1700,
                    Currency = "RSD",
                    LastUpdate = "634792557112051695",
                    ProductId = 4,
                    PriceListId = 3,
                }
               );
            modelBuilder.Entity<ProductDocument>().HasData(
               new ProductDocument
               {
                   PriceValue = 12,
                   Quantity = 11,
                   Currency = "USD",
                   ProductId = 1,
                   DocumentId = 1,
               }
              );
        }
    }
}