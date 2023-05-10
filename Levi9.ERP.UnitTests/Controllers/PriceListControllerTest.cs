using NUnit.Framework;
using Moq;
using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Datas.Requests;

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

        [Test]
        public async Task GetAllPricesLists_ReturnsOkWithMappedList_WhenServiceReturnsNonEmptyList()
        {
            var priceListDTO1 = new PriceListDTO { Id = 1, Name = "Price List 1" };
            var priceListDTO2 = new PriceListDTO { Id = 2, Name = "Price List 2" };
            var priceListDTOs = new List<PriceListDTO> { priceListDTO1, priceListDTO2 };
            var expectedResponse1 = new PriceListResponse { Id = 1, Name = "Price List 1" };
            var expectedResponse2 = new PriceListResponse { Id = 2, Name = "Price List 2" };
            var expectedResponses = new List<PriceListResponse> { expectedResponse1, expectedResponse2 };

            _priceListServiceMock.Setup(x => x.GetAllPricesLists()).ReturnsAsync(priceListDTOs);
            _mapperMock.Setup(x => x.Map<PriceListResponse>(priceListDTO1)).Returns(expectedResponse1);
            _mapperMock.Setup(x => x.Map<PriceListResponse>(priceListDTO2)).Returns(expectedResponse2);

            var result = await _priceListController.GetAllPricesLists();

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var responseList = okResult.Value as IEnumerable<PriceListResponse>;
            Assert.That(responseList, Is.Not.Null);
            CollectionAssert.AreEqual(expectedResponses, responseList);
        }

        [Test]
        public async Task GetAllPricesLists_ReturnsOkWithEmptyMessage_WhenServiceReturnsEmptyList()
        {
            var emptyList = Enumerable.Empty<PriceListDTO>();
            _priceListServiceMock.Setup(x => x.GetAllPricesLists()).ReturnsAsync(emptyList);

            var result = await _priceListController.GetAllPricesLists();

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo("There is no prices lists in database :( "));
        }
        public async Task GetByGlobalId_ReturnsOkResult_WhenPriceListExists()
        {
            Guid globalId = Guid.NewGuid();
            var priceListDto = new PriceListDTO { GlobalId = globalId, Name = "Test Price List" };
            var priceListResponse = new PriceListResponse { GlobalId = globalId, Name = "Test Price List" };
            _priceListServiceMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync(priceListDto);
            _mapperMock.Setup(x => x.Map<PriceListResponse>(priceListDto)).Returns(priceListResponse);

            var result = await _priceListController.GetByGlobalId(globalId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(priceListResponse));
        }

        [Test]
        public async Task GetByGlobalId_ReturnsNotFound_WhenPriceListDoesNotExist()
        {
            Guid globalId = Guid.NewGuid();
            _priceListServiceMock.Setup(x => x.GetByGlobalIdAsync(globalId)).ReturnsAsync(null as PriceListDTO);

            var result = await _priceListController.GetByGlobalId(globalId);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.Value, Is.EqualTo($"Nonexistent price list with global ID: {globalId}"));
        }
        [Test]
        public async Task AddProductIntoPriceList_ReturnsOkWithPriceResponse_WhenServiceReturnsValidPriceDto()
        {
            var priceRequest = new PriceRequest
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = "USD"
            };
            var priceProductDto = new PriceProductDTO
            {
                PriceListId = priceRequest.PriceListId,
                ProductId = priceRequest.ProductId,
                Price = priceRequest.Price,
                Currency = priceRequest.Currency
            };
            var newPriceProductDto = new PriceProductDTO
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = "USD"
            };
            var priceResponse = new PriceResponse
            {
                PriceListId = priceRequest.PriceListId,
                ProductId = priceRequest.ProductId,
                Price = priceRequest.Price,
                Currency = priceRequest.Currency
            };

            _mapperMock.Setup(x => x.Map<PriceProductDTO>(priceRequest)).Returns(priceProductDto);
            _priceListServiceMock.Setup(x => x.AddPrice(priceProductDto)).ReturnsAsync(newPriceProductDto);
            _mapperMock.Setup(x => x.Map<PriceResponse>(newPriceProductDto)).Returns(priceResponse);

            var result = await _priceListController.AddProductIntoPriceList(priceRequest);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(priceResponse));
        }

        [Test]
        public async Task AddProductIntoPriceList_ReturnsBadRequest_WhenServiceReturnsNullPriceDto()
        {
            var priceRequest = new PriceRequest
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = "USD"
            };
            var priceProductDto = new PriceProductDTO
            {
                PriceListId = priceRequest.PriceListId,
                ProductId = priceRequest.ProductId,
                Price = priceRequest.Price,
                Currency = priceRequest.Currency
            };

            _mapperMock.Setup(x => x.Map<PriceProductDTO>(priceRequest)).Returns(priceProductDto);
            _priceListServiceMock.Setup(x => x.AddPrice(priceProductDto)).ReturnsAsync(null as PriceProductDTO);

            var result = await _priceListController.AddProductIntoPriceList(priceRequest);

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}