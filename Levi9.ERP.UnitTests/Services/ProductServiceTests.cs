using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private ProductService _productService;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<ProductService>> _loggerMock;


        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<ProductService>>();  
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
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

        [Test]
        public async Task GetProductsByParameters_ReturnsExpectedResult()
        {
            // Arrange
            var searchParams = new SearchProductDTO
            {
                Name = "product name",
                Page = 1,
                OrderBy = "name",
                Direction = "ASC"
            };
            var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                GlobalId = Guid.NewGuid(),
                Name = "product name",
                ImageUrl = "http://example.com/image.jpg",
                AvailableQuantity = 10,
                LastUpdate = "123454327639523856",
                Prices = new List<Price>
                {
                    new Price
                    {
                        ProductId = 1,
                        PriceListId = 1,
                        GlobalId =  Guid.NewGuid(),
                        PriceValue = 9.99f,
                        Currency = "USD",
                        LastUpdate = "123454327639523856",
                        PriceList = new PriceList
                        {
                            Id = 1,
                            GlobalId = Guid.NewGuid(),
                            Name = "USD Price List"
                        }
                    }
                }
            }
        };

            ProductDTO retProductDto = new ProductDTO
            {
                Id = products[0].Id,
                GlobalId = products[0].GlobalId,
                Name = products[0].Name,
                ImageUrl = products[0].ImageUrl,
                AvailableQuantity = products[0].AvailableQuantity,
                LastUpdate = products[0].LastUpdate,
                PriceList = new List<PriceDTO>
                {
                    new PriceDTO
                    {
                        Id = 1,
                        GlobalId = Guid.NewGuid(),
                        PriceValue = products[0].Prices[0].PriceValue,
                        Currency = products[0].Prices[0].Currency,
                        LastUpdate = products[0].LastUpdate
                    }
                }
            };

            _productRepositoryMock.Setup(x => x.GetProductsByParameters(
                searchParams.Name, searchParams.Page, searchParams.OrderBy, searchParams.Direction))
                .ReturnsAsync(products);

            _mapperMock.Setup(mock => mock.Map<ProductDTO>(It.IsAny<Product>()))
                .Returns(retProductDto);

            // Act
            var result = await _productService.GetProductsByParameters(searchParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var productDTO = result.First();
            Assert.AreEqual(products.First().Id, productDTO.Id);
            Assert.AreEqual(products.First().Name, productDTO.Name);
            Assert.AreEqual(products.First().ImageUrl, productDTO.ImageUrl);
            Assert.AreEqual(products.First().AvailableQuantity, productDTO.AvailableQuantity);
            Assert.AreEqual(products.First().LastUpdate, productDTO.LastUpdate);
            Assert.IsNotNull(productDTO.PriceList);
            Assert.AreEqual(1, productDTO.PriceList.Count);
            var priceDTO = productDTO.PriceList.First();
            Assert.AreEqual(products.First().Prices.First().PriceValue, priceDTO.PriceValue);
            Assert.AreEqual(products.First().Prices.First().Currency, priceDTO.Currency);
            Assert.AreEqual(products.First().Prices.First().LastUpdate, priceDTO.LastUpdate);
        }

        [Test]
        public async Task GetProductsByParameters_ProductsNotFound()
        {
            // Arrange
            var requestDTO = new SearchProductDTO
            {
                Page = 1,
                Name = "non-existing-product",
                OrderBy = "price",
                Direction = "asc"
            };

            var products = new List<Product>();

            _productRepositoryMock.Setup(x => x.GetProductsByParameters(requestDTO.Name, requestDTO.Page, requestDTO.OrderBy, requestDTO.Direction))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetProductsByParameters(requestDTO);

            // Assert
            Assert.That(result, Is.InstanceOf<IEnumerable<ProductDTO>>());
            Assert.That(result.Any(), Is.False);
        }

    }
}
