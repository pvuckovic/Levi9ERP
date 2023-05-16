using NUnit.Framework;
using Moq;
using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Microsoft.AspNetCore.Mvc;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Levi9.ERP.UnitTests.Services
{
    [TestFixture]
    public class PriceListServiceTests
    {
        private PriceListService _priceListService;
        private Mock<IPriceListRepository> _priceListRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<PriceListService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _priceListRepositoryMock = new Mock<IPriceListRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<PriceListService>>();
            _priceListService = new PriceListService(_priceListRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsPriceListDto_WhenIdIsValid()
        {
            int id = 1;
            var expectedPriceList = new PriceList { Id = id, Name = "Test Price List" };
            var expectedPriceListDto = new PriceListDTO { Id = id, Name = "Test Price List" };
            _priceListRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedPriceList);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(expectedPriceList)).Returns(expectedPriceListDto);

            var result = await _priceListService.GetByIdAsync(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PriceListDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(expectedPriceListDto.Id));
                Assert.That(result.Name, Is.EqualTo(expectedPriceListDto.Name));
            });
        }

        [Test]
        public async Task GetByIdAsync_ReturnsNull_WhenPriceListNotFound()
        {
            int id = 1;
            _priceListRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as PriceList);

            var result = await _priceListService.GetByIdAsync(id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllPricesLists_ReturnsMappedList_WhenRepositoryReturnsNonEmptyList()
        {
            var priceList1 = new PriceList { Id = 1, Name = "Price List 1" };
            var priceList2 = new PriceList { Id = 2, Name = "Price List 2" };
            var priceLists = new List<PriceList> { priceList1, priceList2 };
            var expectedDTO1 = new PriceListDTO { Id = 1, Name = "Price List 1" };
            var expectedDTO2 = new PriceListDTO { Id = 2, Name = "Price List 2" };
            var expectedDTOs = new List<PriceListDTO> { expectedDTO1, expectedDTO2 };

            _priceListRepositoryMock.Setup(x => x.GetAllPricesLists()).ReturnsAsync(priceLists);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(priceList1)).Returns(expectedDTO1);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(priceList2)).Returns(expectedDTO2);

            var result = await _priceListService.GetAllPricesLists();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PriceListDTO>>());
            Assert.That(result.Count(), Is.EqualTo(expectedDTOs.Count));
            CollectionAssert.AreEqual(expectedDTOs, result);
        }
    
        [Test]
        public async Task GetAllPricesLists_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
        {
            var emptyList = new List<PriceList>();
            _priceListRepositoryMock.Setup(x => x.GetAllPricesLists()).ReturnsAsync(emptyList);

            var result = await _priceListService.GetAllPricesLists();


            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PriceListDTO>>());
            Assert.That(result.Any(), Is.False);
        }
        [Test]
        public async Task GetByGlobalIdAsync_ReturnsPriceListDto_WhenGlobalIdIsValid()
        {
            Guid globalId = Guid.NewGuid();
            var expectedPriceList = new PriceList { GlobalId = globalId, Name = "Test Price List" };
            var expectedPriceListDto = new PriceListDTO { GlobalId = globalId, Name = "Test Price List" };
            _priceListRepositoryMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync(expectedPriceList);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(expectedPriceList)).Returns(expectedPriceListDto);

            var result = await _priceListService.GetByGlobalIdAsync(globalId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PriceListDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(result.GlobalId, Is.EqualTo(expectedPriceListDto.GlobalId));
                Assert.That(result.Name, Is.EqualTo(expectedPriceListDto.Name));
            });
        }
        [Test]
        public async Task GetByGlobalIdAsync_ReturnsNull_WhenPriceListNotFound()
        {
            Guid globalId = Guid.NewGuid();
            _priceListRepositoryMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync(null as PriceList);

            var result = await _priceListService.GetByGlobalIdAsync(globalId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddPrice_ReturnsNewPriceProductDto_WhenRepositoryAddsPrice()
        {
            var priceProductDto = new PriceProductDTO
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = CurrencyType.USD
            };
            var price = new Price
            {
                PriceListId = priceProductDto.PriceListId,
                ProductId = priceProductDto.ProductId,
                PriceValue = priceProductDto.Price,
                Currency = priceProductDto.Currency.ToString()
            };

            var newPrice = new Price
            {
                PriceListId = 1,
                ProductId = 1,
                PriceValue = 9.99f,
                Currency = "USD"
            };

            var newPriceProductDto = new PriceProductDTO
            {
                PriceListId = newPrice.PriceListId,
                ProductId = newPrice.ProductId,
                Price = newPrice.PriceValue,
            };
            Enum.TryParse(typeof(CurrencyType), price.Currency, out object currency);
            newPriceProductDto.Currency = (CurrencyType)currency;

            _mapperMock.Setup(x => x.Map<Price>(priceProductDto)).Returns(price);
            _priceListRepositoryMock.Setup(x => x.AddPrice(price)).ReturnsAsync(newPrice);
            _mapperMock.Setup(x => x.Map<PriceProductDTO>(newPrice)).Returns(newPriceProductDto);

            var result = await _priceListService.AddPrice(priceProductDto);

            Assert.That(result, Is.EqualTo(newPriceProductDto));
        }
        [Test]
        public async Task UpdatePrice_ReturnsNewPriceProductDto_WhenRepositoryUpdatePrice()
        {
            var priceProductDto = new PriceProductDTO
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = CurrencyType.USD
            };
            var price = new Price
            {
                PriceListId = priceProductDto.PriceListId,
                ProductId = priceProductDto.ProductId,
                PriceValue = priceProductDto.Price,
                Currency = priceProductDto.Currency.ToString()
            };

            var newPrice = new Price
            {
                PriceListId = 1,
                ProductId = 1,
                PriceValue = 9.99f,
                Currency = "USD"
            };

            var updatedPriceProductDto = new PriceProductDTO
            {
                PriceListId = newPrice.PriceListId,
                ProductId = newPrice.ProductId,
                Price = newPrice.PriceValue,
            };
            Enum.TryParse(typeof(CurrencyType), price.Currency, out object currency);
            updatedPriceProductDto.Currency = (CurrencyType)currency;

            _mapperMock.Setup(x => x.Map<Price>(priceProductDto)).Returns(price);
            _priceListRepositoryMock.Setup(x => x.UpdatePrice(price)).ReturnsAsync(newPrice);
            _mapperMock.Setup(x => x.Map<PriceProductDTO>(newPrice)).Returns(updatedPriceProductDto);

            var result = await _priceListService.UpdatePrice(priceProductDto);

            Assert.That(result, Is.EqualTo(updatedPriceProductDto));
        }
        [Test]
        public async Task SearchArticle_ReturnsSearchResults()
        {
            var searchArticleDTO = new SearchArticleDTO
            {
                PageId = 1,
                SearchString = "price",
                OrderBy = OrderByArticleType.ProductId,
                Direction = DirectionType.ASC
            };

            var priceListArticleDTOs = new List<PriceListArticleDTO>
            {
                new PriceListArticleDTO
                {
                    Id = 1,
                    GlobalId = Guid.NewGuid(),
                    Name = "Price list 1",
                    Articles = new List<ArticleDTO>
                    {
                       new ArticleDTO(){ Id= 1, GlobalId = Guid.NewGuid(), Name = "Article1", Price = 100, Currency = CurrencyType.RSD },
                       new ArticleDTO(){ Id= 2, GlobalId = Guid.NewGuid(), Name = "Article2", Price = 200, Currency = CurrencyType.RSD }
                    },
                    LastUpdate = "12341241521512512",
                },
                new PriceListArticleDTO
                {
                    Id = 2,
                    GlobalId = Guid.NewGuid(),
                    Name = "Price list 2",
                    Articles = new List<ArticleDTO>
                    {
                       new ArticleDTO(){ Id= 1, GlobalId = Guid.NewGuid(), Name = "Article1", Price = 123, Currency = CurrencyType.RSD },
                       new ArticleDTO(){ Id= 2, GlobalId = Guid.NewGuid(), Name = "Article2", Price = 5125, Currency = CurrencyType.RSD }
                    },
                    LastUpdate = "123412415215125123",
                },
            };

            _priceListRepositoryMock.Setup(x => x.SearchArticle(
                searchArticleDTO.PageId,
                searchArticleDTO.SearchString,
                searchArticleDTO.OrderBy,
                searchArticleDTO.Direction))
                .ReturnsAsync(priceListArticleDTOs);

            var result = await _priceListService.SearchArticle(searchArticleDTO);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PriceListArticleDTO>>());
            Assert.That(result.Count(), Is.EqualTo(priceListArticleDTOs.Count));
        }
    }
}