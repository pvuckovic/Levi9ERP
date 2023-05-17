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
            Assert.That(result , Is.EqualTo("\"Email already exists\""));
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
    }
}
