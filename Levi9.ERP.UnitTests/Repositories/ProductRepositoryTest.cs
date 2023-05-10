using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Bogus;
using FluentAssertions;
using System.Net.Sockets;

namespace Levi9.ERP.UnitTests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private List<Product> _stub;
        private IProductRepository _repository;
        private Mock<DataBaseContext> _mockProductDbContext;
        private Mock<DbSet<Product>> _mockProductSet;
        private Mock<DbSet<Price>> _mockPriceSet;
        private Mock<DbSet<PriceList>> _mockPriceListSet;

        [SetUp]
        public void Setup()
        {
            _stub = GenerateData(1);
            var data = _stub.AsQueryable();
            _mockProductDbContext = new Mock<DataBaseContext>();
            _mockProductSet = new Mock<DbSet<Product>>();
            _mockProductSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockProductSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockProductSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockProductSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            
            //_mockPriceSet = new Mock<DbSet<Price>>();
            //_mockPriceSet.As<IQueryable<Price>>().Setup(m => m.Provider).Returns(data.Provider);
            //_mockPriceSet.As<IQueryable<Price>>().Setup(m => m.Expression).Returns(data.Expression);
            //_mockPriceSet.As<IQueryable<Price>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //_mockPriceSet.As<IQueryable<Price>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            _mockProductDbContext.Setup(x => x.Products).Returns(_mockProductSet.Object);
            _mockProductDbContext.Setup(x => x.FindAsync(It.IsAny<Type>())).ReturnsAsync(new Product[] { });

            _repository = new ProductRepository(_mockProductDbContext.Object);
        }

        [Test]
        public async Task add_product_should_add_product_to_database()
        {
            // Arrange
            var product = _stub.FirstOrDefault();
            // Act
            var response = await _repository.AddProductAsync(product);
            // Assert
            _mockProductSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            _mockProductDbContext.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Once());
            _mockProductSet.Verify(m => m.Add(It.Is<Product>(p => p.Name == product.Name)), Times.Once());

        }

        //[Test]
        //public async Task get_product_by_id_should_return_correct_product()
        //{
        //    // Arrange
        //    var productId = 1;
        //    var product = _stub.FirstOrDefault();
        //    // Act
        //    var response = await _repository.GetProductById(productId);
        //    // Assert
        //    response.Should().NotBeNull();
        //    response.Id.Should().Be(productId);
        //    response.Name.Should().Be(product.Name);
        //    response.GlobalId.Should().Be(product.GlobalId);
        //    response.ImageUrl.Should().Be(product.ImageUrl);
        //    response.AvailableQuantity.Should().Be(product.AvailableQuantity);
        //    response.LastUpdate.Should().Be(product.LastUpdate);
        //}




        private List<Product> GenerateData(int count)
        {
            var faker = new Faker<Product>()
                         .RuleFor(c => c.Name, f => f.Person.FirstName)
                         .RuleFor(c => c.Id, 1)
                         .RuleFor(c => c.GlobalId, Guid.NewGuid())
                         .RuleFor(c => c.ImageUrl, "image.url")
                         .RuleFor(c => c.AvailableQuantity, 15000)
                         .RuleFor(c => c.LastUpdate, DateTime.Now.ToFileTimeUtc().ToString());

            return faker.Generate(count);
        }

        private List<Price> GeneratePrices(int count)
        {
            var priceFaker = new Faker<Price>()
                .RuleFor(c => c.Currency, "EUR")
                .RuleFor(c => c.ProductId, 1)
                .RuleFor(c => c.LastUpdate, DateTime.Now.ToFileTimeUtc().ToString())
                .RuleFor(c => c.PriceValue, 1.99f)
                .RuleFor(c => c.PriceListId, 1);

            return priceFaker.Generate(count);
        }

        private List<PriceList> GeneratePriceList(int count)
        {
            var priceFaker = new Faker<PriceList>()
                .RuleFor(c => c.Name, "EUR PriceList")
                .RuleFor(c => c.GlobalId, Guid.NewGuid())
                .RuleFor(c => c.LastUpdate, DateTime.Now.ToFileTimeUtc().ToString())
                .RuleFor(c => c.Id, 1);

            return priceFaker.Generate(count);
        }

        private List<Product> GenerateDataAll(int count)
        {
            var faker = new Faker<Product>()
                .RuleFor(c => c.Name, f => f.Person.FirstName)
                .RuleFor(c => c.Id, 1)
                .RuleFor(c => c.GlobalId, Guid.NewGuid())
                .RuleFor(c => c.ImageUrl, "image.url")
                .RuleFor(c => c.AvailableQuantity, 15000)
                .RuleFor(c => c.LastUpdate, DateTime.Now.ToFileTimeUtc().ToString())
                .RuleFor(c => c.Prices, GeneratePrices(1));

            return faker.Generate(count);
        }

    }
}
