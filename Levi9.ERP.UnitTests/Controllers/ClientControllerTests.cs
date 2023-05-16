using AutoMapper;
using Castle.Core.Logging;
using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Mappers;
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
        private IMapper _mapper;
        private Mock<IUrlHelper> _urlHelperMock;
        private ClientController _clientController;
        private Mock<ILogger<ClientController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _mockClientService = new Mock<IClientService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfiles());
            }).CreateMapper();
            _urlHelperMock = new Mock<IUrlHelper>();
            _loggerMock = new Mock<ILogger<ClientController>>();

            _clientController = new ClientController(_mockClientService.Object, _mapper, _urlHelperMock.Object, _loggerMock.Object);
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
    }
}
