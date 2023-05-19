﻿using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Controllers
{
    [TestFixture]
    public class ClientControllerTests
    {
        private Mock<IClientService> _mockClientService;
        private Mock<IMapper> _mockMapper;
        private Mock<IUrlHelper> _urlHelperMock;
        private ClientController _clientController;
        private Mock<ILogger<ClientController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockClientService = new Mock<IClientService>();
            _urlHelperMock = new Mock<IUrlHelper>();
            _loggerMock = new Mock<ILogger<ClientController>>();

            _clientController = new ClientController(_mockClientService.Object, _mockMapper.Object, _urlHelperMock.Object, _loggerMock.Object);
            _clientController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public async Task CreateClient_ValidClient_ReturnsCreatedResponse()
        {
            var clientRequest = new ClientRequest
            {
                Name = "John",
                Email = "john@test.com"
            };

            var clientDTO = new ClientDTO
            {
                Id = 1,
                Name = "John",
                Email = "john@test.com"
            };
            _urlHelperMock.Setup(x => x.Action(It.Is<UrlActionContext>(uac =>
                                                uac.Action == "CreateClient" &&
                                                uac.Controller == "Client" &&
                                                uac.Protocol == string.Empty)))
                                                .Returns("callbackUrl");
            _mockClientService.Setup(x => x.CreateClient(It.IsAny<ClientDTO>())).ReturnsAsync(clientDTO);

            var result = await _clientController.CreateClient(clientRequest);

            Assert.IsInstanceOf<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(clientDTO.Name, ((ClientResponse)createdResult.Value).Name);
            Assert.AreEqual(clientDTO.Email, ((ClientResponse)createdResult.Value).Email);
        }

        [Test]
        public async Task CreateClient_Email_Already_Exists_ReturnsBadRequest()
        {
            var clientRequest = new ClientRequest
            {
                Name = "John",
                Email = "john@test.com"
            };

            var clientDTO = new ClientDTO
            {
                Id = 1,
                Name = "John",
                Email = "john@test.com"
            };
            _mockClientService.Setup(s => s.GetClientByEmail(clientRequest.Email)).ReturnsAsync(clientDTO);

            var result = await _clientController.CreateClient(clientRequest);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Email already exists", badRequestResult.Value);

        }
        [Test]
        public async Task GetClientById_With_Valid_Id_ReturnsOk()
        {
            int clientId = 1;
            var clientDTO = new ClientDTO { Id = clientId, Name = "John Doe" };
            var clientResponse = new ClientResponse { ClientId = clientId, Name = "John Doe" };
            _mockClientService.Setup(service => service.GetClientById(clientId)).ReturnsAsync(clientDTO);

            var result = await _clientController.GetClientById(clientId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var createdResult = (OkObjectResult)result;
            Assert.AreEqual(200, createdResult.StatusCode);

        }

        [Test]
        public async Task GetClientById_With_Invalid_Id_ReturnsNotFound()
        {
            int clientId = 5;
            _mockClientService.Setup(service => service.GetClientById(clientId)).ReturnsAsync((ClientDTO)null);

            var result = await _clientController.GetClientById(clientId);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("User not found", notFoundResult.Value);

        }
        [Test]
        public async Task GetAllClients_ReturnsOkWithMappedList_WhenServiceReturnsNonEmptyList()
        {
            var lastUpdate = "123288706851213387";
            var clientDto1 = new ClientDTO { Id = 1, Name = "Client 1" };
            var clientDto2 = new ClientDTO { Id = 2, Name = "Client 2" };
            var clientsDto = new List<ClientDTO> { clientDto1, clientDto2 };
            var expectedResponse1 = new ClientResponse { ClientId = 1, Name = "Client 1" };
            var expectedResponse2 = new ClientResponse { ClientId = 2, Name = "Client 2" };
            var expectedResponses = new List<ClientResponse> { expectedResponse1, expectedResponse2 };

            _mockClientService.Setup(x => x.GetClientsByLastUpdate(lastUpdate)).ReturnsAsync(clientsDto);
            _mockMapper.Setup(x => x.Map<ClientResponse>(clientDto1)).Returns(expectedResponse1);
            _mockMapper.Setup(x => x.Map<ClientResponse>(clientDto2)).Returns(expectedResponse2);

            var result = await _clientController.GetAllClients(lastUpdate);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var responseList = okResult.Value as IEnumerable<ClientResponse>;
            Assert.That(responseList, Is.Not.Null);
            CollectionAssert.AreEqual(expectedResponses, responseList);
        }
        [Test]
        public async Task GetAllClients_ReturnsOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            var lastUpdate = "833288706851213387";
            var emptyList = Enumerable.Empty<ClientDTO>();
            _mockClientService.Setup(x => x.GetClientsByLastUpdate(lastUpdate)).ReturnsAsync(emptyList);

            var result = await _clientController.GetAllClients(lastUpdate);

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var responseList = okResult.Value as IEnumerable<ClientDTO>;
            CollectionAssert.AreEqual(responseList, emptyList);
        }
    }
}
