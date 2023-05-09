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

            var result = await _priceListSrvice.GetAllPricesLists();

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

            var result = await _priceListSrvice.GetAllPricesLists();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PriceListDTO>>());
            Assert.That(result.Any(), Is.False);
        }
    }
}
