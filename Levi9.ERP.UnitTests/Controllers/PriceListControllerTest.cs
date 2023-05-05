using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Data.Response;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.UnitTests.Controllers
{
    [TestFixture]
    public class PriceListControllerTest
    {
        
        private Mock<IPriceListService> iPriceListServiceMock;
        private Mock<IMapper> _mapperMock;
        private PriceListController _priceListController;
        
        [SetUp]
        public void Setup()
        {
            iPriceListServiceMock = new Mock<IPriceListService>();
            _mapperMock = new Mock<IMapper>();
            _priceListController = new PriceListController(iPriceListServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Get_ReturnsOkObjectResult_WhenServiceReturnsPriceList()
        {
            int id = 1;
            var expectedPriceListDto = new PriceListDTO { Id = id,GlobalId = Guid.NewGuid() ,Name = "Test Price List", LastUpdate = "123654789987654321" };
            var expectedPriceListResponse = new PriceListResponse { Id = id, GlobalId = Guid.NewGuid(), Name = "Test Price List", LastUpdate = "123654789987654321" };
            iPriceListServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedPriceListDto);
            _mapperMock.Setup(x => x.Map<PriceListResponse>(expectedPriceListDto)).Returns(expectedPriceListResponse);

            var result = await _priceListController.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedPriceListResponse, okResult.Value);
        }

        [Test]
        public async Task Get_ReturnsBadRequestObjectResult_WhenServiceThrowsInvalidOperationException()
        {
            int id = 1;
            iPriceListServiceMock.Setup(x => x.GetByIdAsync(id)).ThrowsAsync(new InvalidOperationException());

            var result = await _priceListController.Get(id);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo($"Non existent price list with ID: {id}"));
        }

        [Test]
        public async Task Get_ReturnsBadRequestObjectResult_WhenServiceThrowsArgumentException()
        {
            int id = 0;
            iPriceListServiceMock.Setup(x => x.GetByIdAsync(id)).ThrowsAsync(new ArgumentException());

            var result = await _priceListController.Get(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual($"Invalid number({id}) of ID", badRequestResult.Value);
        }
    }
}
