using Levi9.ERP.Controllers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    [TestFixture]
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _clientRepositoryMock;
        private ClientService _clientService;
        private Mock<ILogger<ClientService>> _loggerMock;


        [SetUp]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<ILogger<ClientService>>();
            _clientService = new ClientService(_clientRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task CreateClient_ValidClient_ReturnsDTO()
        {
            var clientModel = new ClientDTO
            {
                Email = "john.@test.com",
                PriceListId = 1
            };
            var createdClient = new ClientDTO
            {
                GlobalId = clientModel.GlobalId,
                Email = clientModel.Email,
            };
            _clientRepositoryMock.Setup(x => x.AddClient(clientModel)).ReturnsAsync(createdClient);

            var result = await _clientService.CreateClient(clientModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(clientModel.Email, result.Email);
        }
        [Test]
        public async Task GetByIdClient_ReturnsDTO()
        {
            var clientId = 1;
            var clientEntity = new ClientDTO
            {
                Id = clientId,
                Name = "John",
                Email = "john@example.com"
            };
            var expectedClientDTO = new ClientDTO
            {
                Id = clientId,
                Name = "John",
                Email = "john@example.com"
            };

            _clientRepositoryMock.Setup(x => x.GetClientById(clientId)).ReturnsAsync(clientEntity);

            var result = await _clientService.GetClientById(clientId);

            Assert.IsInstanceOf<ClientDTO>(result);
            var actualClientDTO = result;
            Assert.AreEqual(expectedClientDTO.Id, actualClientDTO.Id);
            Assert.AreEqual(expectedClientDTO.Name, actualClientDTO.Name);
            Assert.AreEqual(expectedClientDTO.Email, actualClientDTO.Email);
        }
    }
}
