using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Datas.Requests;
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
        private Mock<IMapper> _mapper;


        [SetUp]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<ILogger<ClientService>>();
            _mapper = new Mock<IMapper>();
            _clientService = new ClientService(_clientRepositoryMock.Object, _loggerMock.Object, _mapper.Object);
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
        [Test]
        public async Task SyncClients_WithNonExistingEmails_ReturnsLastUpdateTimestamp()
        {
            // Arrange
            var clients = new List<ClientSyncRequestDTO>
            {
                new ClientSyncRequestDTO { GlobalId = Guid.NewGuid(), Email = "test1@example.com" },
                new ClientSyncRequestDTO { GlobalId = Guid.NewGuid(), Email = "test2@example.com" },
                // Add more client DTOs as needed
            };

            var lastUpdate = DateTime.Now.ToFileTimeUtc().ToString();

            _clientRepositoryMock.Setup(r => r.DoesClientEmailAlreadyExists(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(false);
            _clientRepositoryMock.Setup(r => r.DoesClientByGlobalIdExists(It.IsAny<Guid>())).ReturnsAsync(false);
           //_clientRepositoryMock.Setup(r => r.AddClient(It.IsAny<ClientDTO>())).Returns(Task.CompletedTask);

            _mapper.Setup(m => m.Map<ClientDTO>(It.IsAny<ClientSyncRequestDTO>())).Returns(new ClientDTO());

            // Act
            var result = await _clientService.SyncClients(clients);

            // Assert
            Assert.IsNotNull(result);
            _clientRepositoryMock.Verify(r => r.DoesClientEmailAlreadyExists(It.IsAny<Guid>(), It.IsAny<string>()), Times.Exactly(clients.Count));
            _clientRepositoryMock.Verify(r => r.DoesClientByGlobalIdExists(It.IsAny<Guid>()), Times.Exactly(clients.Count));
            _clientRepositoryMock.Verify(r => r.AddClient(It.IsAny<ClientDTO>()), Times.Exactly(clients.Count));
            _clientRepositoryMock.Verify(r => r.UpdateClient(It.IsAny<ClientSyncRequestDTO>()), Times.Never);
        }

        [Test]
        public async Task SyncClients_WithExistingEmails_ReturnsNull()
        {
            // Arrange
            var clients = new List<ClientSyncRequestDTO>
            {
                new ClientSyncRequestDTO { GlobalId = Guid.NewGuid(), Email = "test1@example.com" },
                new ClientSyncRequestDTO { GlobalId = Guid.NewGuid(), Email = "test2@example.com" },
            };

            _clientRepositoryMock.Setup(r => r.DoesClientEmailAlreadyExists(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _clientService.SyncClients(clients);

            // Assert
            Assert.IsNull(result);
            _clientRepositoryMock.Verify(r => r.DoesClientEmailAlreadyExists(It.IsAny<Guid>(), It.IsAny<string>()), Times.Exactly(1));
            _clientRepositoryMock.Verify(r => r.DoesClientByGlobalIdExists(It.IsAny<Guid>()), Times.Never);
            _clientRepositoryMock.Verify(r => r.AddClient(It.IsAny<ClientDTO>()), Times.Never);
            _clientRepositoryMock.Verify(r => r.UpdateClient(It.IsAny<ClientSyncRequestDTO>()), Times.Never);
        }
    }
}
