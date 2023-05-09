using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductService _productService;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateProductAsync_WithValidName_ShouldReturnProductDTO()
        {
            // Arrange
            var productDTO = new ProductDTO
            {
                Name = "Product Name",
                ImageUrl = "someurl.png",
            };
            var product = new Product
            {
                Name = productDTO.Name,
                GlobalId = Guid.NewGuid(),
                ImageUrl = productDTO.ImageUrl,
                AvailableQuantity = 15000,
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString(),
            };
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(productDTO);
            _productRepositoryMock.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _productService.CreateProductAsync(productDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProductDTO>(result);
            Assert.AreEqual(productDTO.Name, result.Name);
            Assert.AreEqual(productDTO.ImageUrl, result.ImageUrl);
            Assert.AreEqual(productDTO.PriceList, result.PriceList);


        }

        [Test]
        public async Task GetProductByName_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            string productName = "Test Product";
            Product product = new Product { Name = productName };
            ProductDTO expectedProductDto = new ProductDTO { Name = productName };
            _productRepositoryMock.Setup(repo => repo.GetProductByName(productName)).ReturnsAsync(product);
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(expectedProductDto);
            // Act
            ProductDTO actualProductDto = await _productService.GetProductByName(productName);
            // Assert
            Assert.AreEqual(expectedProductDto.Name, actualProductDto.Name);
        }

        [Test]
        public async Task GetProductByName_WhenProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            string name = "Product1";
            _productRepositoryMock.Setup(repo => repo.GetProductByName(name)).ReturnsAsync((Product)null);
            // Act
            var result = await _productService.GetProductByName(name);
            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetProductByGlobalId_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Product product = new Product { GlobalId = productId };
            ProductDTO expectedProductDto = new ProductDTO { GlobalId = productId };
            _productRepositoryMock.Setup(repo => repo.GetProductByGlobalId(productId)).ReturnsAsync(product);
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(expectedProductDto);
            // Act
            ProductDTO actualProductDto = await _productService.GetProductByGlobalId(productId);
            // Assert
            Assert.AreEqual(expectedProductDto.GlobalId, actualProductDto.GlobalId);
        }

        [Test]
        public async Task GetProductByGlobalId_WhenProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            _productRepositoryMock.Setup(repo => repo.GetProductByGlobalId(productId)).ReturnsAsync((Product)null);
            // Act
            var result = await _productService.GetProductByGlobalId(productId);
            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetProductById_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            int productId = 1;
            Product product = new Product { Id = productId };
            ProductDTO expectedProductDto = new ProductDTO { Id = productId };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(expectedProductDto);
            // Act
            ProductDTO actualProductDto = await _productService.GetProductById(productId);
            // Assert
            Assert.AreEqual(expectedProductDto.Id, actualProductDto.Id);
        }

        [Test]
        public async Task GetProductById_WhenProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            int productId = 1;
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync((Product)null);
            // Act
            var result = await _productService.GetProductById(productId);
            // Assert
            Assert.Null(result);
        }


    }
}
