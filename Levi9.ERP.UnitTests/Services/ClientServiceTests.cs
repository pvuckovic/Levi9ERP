using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    [TestFixture]
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _clientRepositoryMock;
        private ClientService _clientService;

        [SetUp]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _clientService = new ClientService(_clientRepositoryMock.Object);
        }

        [Test]
        public void CreateClient_ValidClient_ReturnsDTO()
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
            _clientRepositoryMock.Setup(x => x.AddClient(clientModel)).Returns(createdClient);

            var result = _clientService.CreateClient(clientModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(clientModel.Email, result.Email);
        }
        [Test]
        public void GetByIdClient_ReturnsDTO()
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

            _clientRepositoryMock.Setup(x => x.GetClientById(clientId)).Returns(clientEntity);

            var result = _clientService.GetClientById(clientId);

            Assert.IsInstanceOf<ClientDTO>(result);
            var actualClientDTO = result as ClientDTO;
            Assert.AreEqual(expectedClientDTO.Id, actualClientDTO.Id);
            Assert.AreEqual(expectedClientDTO.Name, actualClientDTO.Name);
            Assert.AreEqual(expectedClientDTO.Email, actualClientDTO.Email);
        }
    }
}
