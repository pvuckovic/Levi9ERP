using NUnit.Framework;
using Moq;
using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Levi9.ERP.Domain.Services;

namespace Levi9.ERP.UnitTests.Controllers
{
    [TestFixture]
    public class PriceListControllerTests
    {
        private PricelistController _priceListController;
        private Mock<IPriceListService> _priceListServiceMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _priceListServiceMock = new Mock<IPriceListService>();
            _mapperMock = new Mock<IMapper>();
            _priceListController = new PricelistController(_priceListServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Get_ReturnsOk_WhenIdIsValid()
        {
            int id = 1;
            var expectedPriceListDto = new PriceListDTO { Id = id, Name = "Test Price List" };
            var expectedPriceListResponse = new PriceListResponse { Id = id, Name = "Test Price List" };
            _priceListServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedPriceListDto);
            _mapperMock.Setup(x => x.Map<PriceListResponse>(expectedPriceListDto)).Returns(expectedPriceListResponse);

            var result = await _priceListController.Get(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(expectedPriceListResponse));
        }

        [Test]
        public async Task Get_ReturnsNotFound_WhenPriceListDoesNotExist()
        {
            int id = 1;
            _priceListServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as PriceListDTO);

            var result = await _priceListController.Get(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo($"Nonexistent price list with ID: {id}"));
        }

        [Test]
        public async Task Get_ReturnsBadRequest_WhenIdIsInvalid()
        {
            int id = 0;

            var result = await _priceListController.Get(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo($"Invalid number({id}) of ID"));
        }
    }
}