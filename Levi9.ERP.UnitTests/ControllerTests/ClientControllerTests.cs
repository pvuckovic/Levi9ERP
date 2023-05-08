using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Service;
using Levi9.ERP.Request;
using Levi9.ERP.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using Moq;
using Levi9.ERP.Domain.Mappers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Levi9.ERP.UnitTests.ControllerTests
{
    [TestFixture]
    public class ClientControllerTests
    {
        private Mock<IClientService> mockClientService;
        private IMapper mapper;
        private Mock<IUrlHelper> urlHelperMock;
        private ClientController clientController;

        [SetUp]
        public void Setup()
        {
            mockClientService = new Mock<IClientService>();
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfiles());
            }).CreateMapper();
            urlHelperMock = new Mock<IUrlHelper>();
            clientController = new ClientController(mockClientService.Object, mapper, urlHelperMock.Object);
            clientController.ControllerContext.HttpContext = new DefaultHttpContext();
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
           
            urlHelperMock.Setup(x => x.Action(It.Is<UrlActionContext>(uac =>
                                                uac.Action == "CreateClient" &&
                                                uac.Controller == "Client" &&
                                                uac.Protocol == String.Empty)))
                                                .Returns("callbackUrl");

            mockClientService.Setup(x => x.CreateClient(It.IsAny<ClientDTO>())).Returns(clientDTO);
            var result = clientController.CreateClient(clientRequest);

            Assert.IsInstanceOf<CreatedResult>(result.Result);
            var createdResult = (CreatedResult)result.Result;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(clientDTO.Name, ((ClientResponse)createdResult.Value).Name);
            Assert.AreEqual(clientDTO.Email, ((ClientResponse)createdResult.Value).Email);
        }

        [Test]
        public void CreateClient_InvalidClient_ReturnsInternalServerError()
        {
            var clientRequest = new ClientRequest
            {
                Name = "",
                Email = ""
            };

            mockClientService.Setup(x => x.CreateClient(It.IsAny<ClientDTO>())).Throws(new Exception("Invalid client"));

            var result = clientController.CreateClient(clientRequest);

            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = (ObjectResult)result.Result;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Invalid client", objectResult.Value);
        }
    }
}
