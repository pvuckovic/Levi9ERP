using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private Mock<IMapper> _mapperMock;
        private ProductController _productController;

        [SetUp]
        public void SetUp()
        {
            _productServiceMock = new Mock<IProductService>();
            _mapperMock = new Mock<IMapper>();
            _productController = new ProductController(_productServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateProductAsync_WithValidProductRequest_ReturnsOk()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                Name = "Product Name",
                ImageUrl = "someurl.png"
            };
            var createdProduct = new ProductDTO
            {
                Name = productRequest.Name,
                GlobalId = Guid.NewGuid(),
                ImageUrl = productRequest.ImageUrl,
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString()
            };
            var expectedProductResponse = new ProductResponse
            {
                Name = createdProduct.Name,
                GlobalId = createdProduct.GlobalId,
                ImageUrl = createdProduct.ImageUrl,
                AvailableQuantity = createdProduct.AvailableQuantity,
                LastUpdate = createdProduct.LastUpdate
            };
            _productServiceMock.Setup(s => s.GetProductByName(productRequest.Name)).ReturnsAsync((ProductDTO)null);
            _productServiceMock.Setup(s => s.CreateProductAsync(It.IsAny<ProductDTO>())).ReturnsAsync(createdProduct);
            _mapperMock.Setup(m => m.Map<ProductDTO>(productRequest)).Returns(createdProduct);
            _mapperMock.Setup(m => m.Map<ProductResponse>(createdProduct)).Returns(expectedProductResponse);
            // Act
            IActionResult result = await _productController.CreateProductAsync(productRequest);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedProductResponse, okResult.Value);
        }


        [Test]
        public async Task CreateProductAsync_WithExistingProductName_ReturnsBadRequest()
        {
            // Arrange
            var productName = "Product Name";
            var existingProduct = new ProductDTO
            {
                Name = productName,
                GlobalId = Guid.NewGuid(),
                ImageUrl = "someurl.png",
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString()
            };
            _productServiceMock.Setup(s => s.GetProductByName(productName)).ReturnsAsync(existingProduct);
            // Act
            IActionResult result = await _productController.CreateProductAsync(new ProductRequest { Name = productName });
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("A product with the same name already exists.", badRequestResult.Value);
        }

        [Test]
        public async Task GetById_WithValidId_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var product = new ProductDTO
            {
                Id = id,
                Name = "Product Name",
                GlobalId = Guid.NewGuid(),
                ImageUrl = "someurl.png",
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString()
            };
            var expectedProductResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                GlobalId = product.GlobalId,
                ImageUrl = product.ImageUrl,
                AvailableQuantity = product.AvailableQuantity,
                LastUpdate = product.LastUpdate
            };
            _productServiceMock.Setup(s => s.GetProductById(id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductResponse>(product)).Returns(expectedProductResponse);
            // Act
            IActionResult result = await _productController.GetById(id);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedProductResponse, okResult.Value);
        }

        [Test]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var id = -1;
            // Act
            IActionResult result = await _productController.GetById(id);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Id is null or negative number", badRequestResult.Value);
        }

        [Test]
        public async Task GetByGlobalId_WithValidId_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductDTO
            {
                Id = 1,
                Name = "Product Name",
                GlobalId = id,
                ImageUrl = "someurl.png",
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString()
            };
            var expectedProductResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                GlobalId = product.GlobalId,
                ImageUrl = product.ImageUrl,
                AvailableQuantity = product.AvailableQuantity,
                LastUpdate = product.LastUpdate
            };
            _productServiceMock.Setup(s => s.GetProductByGlobalId(id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductResponse>(product)).Returns(expectedProductResponse);
            // Act
            IActionResult result = await _productController.GetByGlobalId(id);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedProductResponse, okResult.Value);
        }

        [Test]
        public async Task GetByGlobalId_WithNonexistentId_ReturnsBadRequest()
        {
            // Arrange
            var nonexistentId = Guid.NewGuid();
            _productServiceMock.Setup(s => s.GetProductByGlobalId(nonexistentId)).ReturnsAsync((ProductDTO)null);
            // Act
            IActionResult result = await _productController.GetByGlobalId(nonexistentId);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("A product with that id doesn't exists.", badRequestResult.Value);
        }

    }
}
