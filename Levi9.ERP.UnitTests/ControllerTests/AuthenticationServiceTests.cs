using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.ControllerTests
{
    [TestFixture]
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IAuthenticatationService> _authenticationServiceMock;
        private ClientService _clientService;

        [SetUp]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _authenticationServiceMock = new Mock<IAuthenticatationService>();
            _clientService = new ClientService(_clientRepositoryMock.Object, _authenticationServiceMock.Object);
        }

        [Test]
        public void CreateClient_ValidClient_ReturnsDTO()
        {
            var clientModel = new ClientDTO
            {
                Email = "john.@test.com",
                Password = "R+AKYqbYP0P/E9P1mCH5SjXOGZPbEVk79fNnUydFmWY=",
                Salt = "RZVxPvmCKmwVTA==",
                PriceListId = 1
            };
            _authenticationServiceMock.Setup(x => x.GenerateRandomSalt(It.IsAny<int>())).Returns(clientModel.Salt);
            _authenticationServiceMock.Setup(x => x.HashPassword(clientModel.Password, clientModel.Salt)).Returns(clientModel.Password);
            var createdClient = new ClientDTO
            {
                GlobalId = clientModel.GlobalId,
                Email = clientModel.Email,
                Password = clientModel.Password,
                Salt = clientModel.Salt,
            };
            _clientRepositoryMock.Setup(x => x.AddClient(clientModel)).Returns(createdClient);

            var result = _clientService.CreateClient(clientModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(clientModel.Email, result.Email);
            Assert.AreEqual(clientModel.Salt, result.Salt);
            Assert.AreEqual(clientModel.Password, result.Password);
        }
    }
}
