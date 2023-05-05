using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.UnitTests.Controller
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductController _productController;

        [SetUp]
        public void SetUp()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Test]
        public async Task CreateProductAsync_WhenProductDoesNotExist_ReturnsOk()
        {
            // Arrange
            var productName = "Test Product";
            _productServiceMock.Setup(s => s.GetProductByName(productName)).ReturnsAsync(() => null);
            _productServiceMock.Setup(s => s.CreateProductAsync(productName)).ReturnsAsync(new Product { Name = productName });
            // Act
            var result = await _productController.CreateProductAsync(productName);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOf<Product>(okResult.Value);
            Assert.AreEqual(productName, (okResult.Value as Product).Name);
        }

        [Test]
        public async Task CreateProductAsync_WhenProductAlreadyExists_ReturnsBadRequest()
        {
            // Arrange
            var productName = "Test Product";
            var existingProduct = new Product { Name = productName };
            _productServiceMock.Setup(s => s.GetProductByName(productName)).ReturnsAsync(existingProduct);
            // Act
            var result = await _productController.CreateProductAsync(productName);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("A product with the same name already exists.", badRequestResult.Value);
        }

        [Test]
        public async Task CreateProductAsync_WithValidName_ReturnsOkResult()
        {
            // Arrange
            var productName = "Test Product";
            var expectedProduct = new Product
            {
                Name = productName,
                GlobalId = Guid.NewGuid(),
                ImageUrl = string.Empty,
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
            };
            _productServiceMock.Setup(service => service.CreateProductAsync(productName))
                .ReturnsAsync(expectedProduct);
            // Act
            var result = await _productController.CreateProductAsync(productName);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual(expectedProduct, okResult.Value);
            _productServiceMock.Verify(service => service.CreateProductAsync(productName), Times.Once);
        }

        [Test]
        public async Task CreateProductAsync_NullName_ReturnsBadRequest()
        {
            // Arrange
            string productName = null;
            _productServiceMock.Setup(s => s.CreateProductAsync(productName)).Throws(new ArgumentException("Product name cannot be null or empty."));
            // Act
            var result = await _productController.CreateProductAsync(productName);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.AreEqual("Product name cannot be null or empty.", badRequestResult.Value);
            _productServiceMock.Verify(service => service.CreateProductAsync(productName), Times.Once);
        }

    }
}
