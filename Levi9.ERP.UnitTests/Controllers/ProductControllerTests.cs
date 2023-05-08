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

    }
}
