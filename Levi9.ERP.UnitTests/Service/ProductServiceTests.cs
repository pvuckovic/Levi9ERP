using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Repository;
using Levi9.ERP.Domain.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.UnitTests.Service
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductService _productService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        //[Test]
        //public async Task CreateProductAsync_ValidName_ReturnsAddedProduct()
        //{
        //    // Arrange
        //    var productName = "Test Product";
        //    var expectedProduct = new Product
        //    {
        //        Name = productName,
        //        GlobalId = Guid.NewGuid(),
        //        ImageUrl = string.Empty,
        //        AvailableQuantity = 15000,
        //        LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
        //    };
        //    _productRepositoryMock.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
        //        .ReturnsAsync(expectedProduct);
        //    // Act
        //    var addedProduct = await _productService.CreateProductAsync(productName);
        //    // Assert
        //    Assert.AreEqual(expectedProduct, addedProduct);
        //    _productRepositoryMock.Verify(repo => repo.AddProductAsync(It.IsAny<Product>()), Times.Once);
        //}

        [Test]
        public void CreateProductAsync_NullName_ThrowsArgumentException()
        {
            // Arrange
            string productName = null;
            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _productService.CreateProductAsync(productName));
        }

        [Test]
        public async Task GetProductByName_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            string productName = "Test Product";
            Product expectedProduct = new Product { Name = productName };
            _productRepositoryMock.Setup(repo => repo.GetProductByName(productName)).ReturnsAsync(expectedProduct);
            // Act
            Product actualProduct = await _productService.GetProductByName(productName);
            // Assert
            Assert.AreEqual(expectedProduct, actualProduct);
        }

        [Test]
        public async Task GetProductByName_WhenProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            string name = "Product1";
            // Act
            var result = await _productService.GetProductByName(name);
            // Assert
            Assert.Null(result);
        }

    }
}
