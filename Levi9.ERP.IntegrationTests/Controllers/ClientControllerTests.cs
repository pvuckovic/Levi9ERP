using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Levi9.ERP.IntegrationTests.Controllers
{
    [TestFixture]
    public class ClientControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private string token;
        [SetUp]
        public void Setup()
        {
            _factory = new TestingWebAppFactory<Program>();
            _client = _factory.CreateClient();
            token = Fixture.GenerateJwt();
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Clients.AddRange(Fixture.GenerateClientData());
                dbContext.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task GetById_ReturnsOk_WhenIdIsValid()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var id = 1;
            var response = await _client.GetAsync($"/v1/Client/{id}");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ClientResponse>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
            Assert.That(result.ClientId, Is.EqualTo(1));
        }
        [Test]
        public async Task CreateClientAsync_ReturnsCreated()
        {
            var clientRequest = new ClientRequest { Name = "Test Client", Address = "test Adress", Email = "testEmail@example.com", Password = "test", Phone = "0606464433", PriceListId = 1 };

            var response = await _client.PostAsJsonAsync("/v1/Client", clientRequest);
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<ClientResponse>(result);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("Test Client", content.Name);
            Assert.AreEqual("testEmail@example.com", content.Email);
        }
        [Test]
        public async Task AddClient_EmailExist_ShouldReturnBadRequest()
        {
            var clientRequest = new ClientRequest { Name = "Test Client", Address = "test Adress", Email = "zlatko123@gmail.com", Password = "test", Phone = "0606464433", PriceListId = 1 };

            var response = await _client.PostAsJsonAsync("v1/Client/", clientRequest);
            var result = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(result, Is.EqualTo("\"Email already exists\""));
        }
        [Test]
        public async Task GetClientById_InvalidId_ReturnsBadRequest()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            int id = 0;

            var response = await _client.GetAsync($"/v1/Client/{id}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task GetClientById_Unauthorized_ReturnsUnauthorized()
        {
            int id = 1;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "uyftghkbvgjcjv");
            var response = await _client.GetAsync($"/v1/client/{id}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
        [Test]
        public async Task GetAllProducts_ReturnsOkWithMappedList_WhenServiceReturnsNonEmptyList()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/v1/Client/sync/133288706851213387");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ClientResponse>>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
        }
        [Test]
        public async Task GetAllProducts_ReturnsOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                dbContext.Clients.RemoveRange(dbContext.Clients);
                dbContext.SaveChanges();
            }
            var response = await _client.GetAsync("/v1/Client/sync/933288706851213387");

            var content = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content, Is.EqualTo("[]"));
            });
        }

        [Test]
        public async Task SyncClients_WithValidData_ReturnsOkResult()
        {
            // Arrange
            // Act
            var clients = new List<ClientSyncRequest>
            {
                new ClientSyncRequest
                {
                    GlobalId = new Guid("B1233B21-ADF0-4148-80AC-8852907419B7"),
                    Name = "Test1 Client",
                    Address = "Test1 Address 123",
                    Email = "test1@example.com",
                    Password = "password",
                    Phone = "0611234567",
                    PriceListId = 1
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(clients);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/v1/client/sync", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task SyncClients_WithInvalidEmailData_ReturnsBadRequest()
        {
            // Arrange
            // Act
            var clients = new List<ClientSyncRequest>
            {
                new ClientSyncRequest
                {
                    GlobalId = new Guid("B1233B21-ADF0-4148-80AC-8852907419B7"),
                    Name = "Test1 Client",
                    Address = "Test1 Address 123",
                    Email = "zlatko123@gmail.com",
                    Password = "password",
                    Phone = "0611234567",
                    PriceListId = 1
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(clients);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/v1/client/sync", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("\"Update failed!\"", result);
        }
    }
}