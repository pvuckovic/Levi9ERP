using NUnit.Framework;
using Moq;
using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Domain.Repositories;

namespace Levi9.ERP.UnitTests.Services
{
    [TestFixture]
    public class PriceListServiceTests
    {
        private PriceListService _priceListSrvice;
        private Mock<IPriceListRepository> _priceListRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _priceListRepositoryMock = new Mock<IPriceListRepository>();
            _mapperMock = new Mock<IMapper>();
            _priceListSrvice = new PriceListService(_priceListRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsPriceListDto_WhenIdIsValid()
        {
            int id = 1;
            var expectedPriceList = new PriceList { Id = id, Name = "Test Price List" };
            var expectedPriceListDto = new PriceListDTO { Id = id, Name = "Test Price List" };
            _priceListRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedPriceList);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(expectedPriceList)).Returns(expectedPriceListDto);

            var result = await _priceListSrvice.GetByIdAsync(id);

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
            _priceListRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((PriceList)null);

            var result = await _priceListSrvice.GetByIdAsync(id);

            Assert.That(result, Is.Null);
        }
        [Test]
        public async Task GetByGlobalIdAsync_ReturnsPriceListDto_WhenGlobalIdIsValid()
        {
            Guid globalId = Guid.NewGuid();
            var expectedPriceList = new PriceList { GlobalId = globalId, Name = "Test Price List" };
            var expectedPriceListDto = new PriceListDTO { GlobalId = globalId, Name = "Test Price List" };
            _priceListRepositoryMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync(expectedPriceList);
            _mapperMock.Setup(x => x.Map<PriceListDTO>(expectedPriceList)).Returns(expectedPriceListDto);

            var result = await _priceListSrvice.GetByGlobalIdAsync(globalId);

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
            _priceListRepositoryMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync((PriceList)null);

            var result = await _priceListSrvice.GetByGlobalIdAsync(globalId);

            Assert.That(result, Is.Null);
        }
    }
}