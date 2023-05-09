
using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Mappers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        [SetUp]
        public void Setup()
        {
            _mockClientService = new Mock<IClientService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfiles());
            }).CreateMapper();
            _urlHelperMock = new Mock<IUrlHelper>();
            _clientController = new ClientController(_mockClientService.Object, _mapper, _urlHelperMock.Object);
            _clientController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public void CreateClient_ValidClient_ReturnsCreatedResponse()
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

            _mockClientService.Setup(x => x.CreateClient(It.IsAny<ClientDTO>())).Returns(clientDTO);
            var result = _clientController.CreateClient(clientRequest);

            Assert.IsInstanceOf<CreatedResult>(result.Result);
            var createdResult = (CreatedResult)result.Result;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(clientDTO.Name, ((ClientResponse)createdResult.Value).Name);
            Assert.AreEqual(clientDTO.Email, ((ClientResponse)createdResult.Value).Email);
        }

    }
}
