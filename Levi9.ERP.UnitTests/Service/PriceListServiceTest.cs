using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Service;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Model;

namespace Levi9.ERP.UnitTests.Service
{
    [TestFixture]
    public class PriceListServiceTest
    {
        private PriceListService _priceListService;
        private Mock<IPriceListRepository> _iPriceListRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _iPriceListRepositoryMock = new Mock<IPriceListRepository>();
            _mapperMock = new Mock<IMapper>();
            _priceListService = new PriceListService(_iPriceListRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsPriceListDto_WhenIdIsValid()
        {
            int id = 1;
            var expectedPriceList = new PriceList { Id = id, Name = "Test Price List" };
            var expectedPriceListDto = new PriceListDTO { Id = id, Name = "Test Price List" };
            _iPriceListRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedPriceList);
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
        public void GetByIdAsync_ThrowsException_WhenIdIsInvalid()
        {
            int id = 0;
            var expectedErrorMessage = "Invalid id";

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _priceListService.GetByIdAsync(id));
            Assert.That(ex.Message, Is.EqualTo(expectedErrorMessage));
        }
    }
}

