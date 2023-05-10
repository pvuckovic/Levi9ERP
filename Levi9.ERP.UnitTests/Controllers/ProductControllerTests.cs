using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain.Models;
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
        public async Task GetByGlobalId_WithNonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentId = Guid.NewGuid();
            _productServiceMock.Setup(s => s.GetProductByGlobalId(nonexistentId)).ReturnsAsync((ProductDTO)null);
            // Act
            IActionResult result = await _productController.GetByGlobalId(nonexistentId);
            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var badRequestResult = (NotFoundObjectResult)result;
            Assert.AreEqual("A product with that id doesn't exists.", badRequestResult.Value);
        }

        [Test]
        public async Task GetById_WithNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 1;
            _productServiceMock.Setup(s => s.GetProductById(nonExistingId)).ReturnsAsync((ProductDTO)null);
            // Act
            var result = await _productController.GetById(nonExistingId);
            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("A product with the same id doesn't exists.", notFoundResult.Value);
        }

        [Test]
        public async Task SearchProducts_ValidSearchParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "name" };
            var products = new List<ProductDTO>
            {
                new ProductDTO {
                    Id = 1,
                    GlobalId = Guid.NewGuid(),
                    Name = "Product 1",
                    ImageUrl = "https://example.com/product1.jpg",
                    AvailableQuantity = 10,
                    LastUpdate = "133277539861042364",
                    PriceList = new List<PriceDTO>
                    {
                        new PriceDTO
                        {
                            Id = 1,
                            GlobalId = Guid.NewGuid(),
                            PriceValue = 9.99f,
                            Currency = "USD",
                            LastUpdate = "133277539861042364"
                        },
                        new PriceDTO
                        {
                            Id = 2,
                            GlobalId = Guid.NewGuid(),
                            PriceValue = 8.99f,
                            Currency = "EUR",
                            LastUpdate = "133277539861042364"
                        }
                    }
                },
                new ProductDTO {
                    Id = 2,
                    GlobalId = Guid.NewGuid(),
                    Name = "Product 2",
                    ImageUrl = "https://example.com/product2.jpg",
                    AvailableQuantity = 5,
                    LastUpdate = "133277539861042364",
                    PriceList = new List<PriceDTO>
                    {
                        new PriceDTO
                        {
                            Id = 3,
                            GlobalId = Guid.NewGuid(),
                            PriceValue = 19.99f,
                            Currency = "USD",
                            LastUpdate = "133277539861042364"
                        },
                        new PriceDTO
                        {
                            Id = 4,
                            GlobalId = Guid.NewGuid(),
                            PriceValue = 17.99f,
                            Currency = "EUR",
                            LastUpdate = "133277539861042364"
                        }
                    }

                }
            };
            _productServiceMock.Setup(s => s.GetProductsByParameters(It.IsAny<SearchProductDTO>()))
                .ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductResponse>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(new List<ProductResponse>());
            // Act
            var result = await _productController.SearchProducts(searchParams);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task SearchProducts_InvalidPage_ReturnsBadRequest()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 0, Name = "test" };
            // Act
            var result = await _productController.SearchProducts(searchParams);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Page must be greater than 0.", badRequestResult.Value);
        }

        [Test]
        public async Task SearchProducts_NoProductsFound_ReturnsNotFound()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "test" };
            _productServiceMock.Setup(s => s.GetProductsByParameters(It.IsAny<SearchProductDTO>()))
                .ReturnsAsync((List<ProductDTO>)null);
            // Act
            var result = await _productController.SearchProducts(searchParams);
            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("No products were found that match the search parameters.", notFoundResult.Value);
        }

    }

}
