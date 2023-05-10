using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Bogus;
using FluentAssertions;
using System.ComponentModel;

namespace Levi9.ERP.UnitTests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private List<Product> _stub;
        private IProductRepository _repository;
        private Mock<DataBaseContext> _mockProductDbContext;

        [SetUp]
        public void Setup()
        {
            _stub = GenerateData(1);
            var data = _stub.AsQueryable();
            _mockProductDbContext = new Mock<DataBaseContext>();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockProductDbContext.Setup(x => x.Products).Returns(mockSet.Object);


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
            response.Should().NotBeNull()
                .And.Subject.Should().BeOfType<Product>()
                .And.Subject.Should().BeSameAs(product);

            var addedProduct = await _mockProductDbContext.Object.Products.FindAsync(product.Id);
            addedProduct.Should().NotBeNull()
                .And.Subject.Should().BeSameAs(product);
        }

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
    }
}
