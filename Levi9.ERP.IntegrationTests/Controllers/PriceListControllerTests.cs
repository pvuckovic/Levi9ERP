using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.IntegrationTests.Controllers
{
    [TestFixture]
    public class PriceListControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new TestingWebAppFactory<Program>();
            _client = _factory.CreateClient();
            string token = Fixture.GenerateJwt();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            var id = 1;
            var response = await _client.GetAsync($"/v1/Pricelist/{id}");
          
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PriceListResponse>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetById_ReturnsNotFound_WhenPriceListDoesNotExist()
        {
            int id = 4;

            var response = await _client.GetAsync($"/v1/Pricelist/{id}");

            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That($"Nonexistent price list with ID: {id}", Is.EqualTo(content));
            });
        }

        [Test]
        public async Task GetById_ReturnsBadRequest_WhenIdIsInvalid()
        {
            int id = 0;

            var response = await _client.GetAsync($"/v1/Pricelist/{id}");

            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That($"Invalid number({id}) of ID", Is.EqualTo(content));
            });
        }

        [Test]
        public async Task GetByGlobalId_ReturnsOkResult_WhenPriceListExists()
        {
            var globalId = new Guid("494ad824-8ee2-47c3-938f-2de7a43db41a");

            var response = await _client.GetAsync($"/v1/Pricelist/global/{globalId}");
            
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PriceListResponse>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
            Assert.That(result.GlobalId, Is.EqualTo(globalId));
        }

        [Test]
        public async Task GetByGlobalId_ReturnsNotFound_WhenPriceListDoesNotExist()
        {
            var globalId = Guid.NewGuid();

            var response = await _client.GetAsync($"/v1/Pricelist/global/{globalId}");

            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(content, Is.EqualTo($"Nonexistent price list with global ID: {globalId}"));
            });
        }

        [Test]
        public async Task GetAllPricesLists_ReturnsOkWithMappedList_WhenServiceReturnsNonEmptyList()
        {
            var response = await _client.GetAsync($"/v1/Pricelist");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<PriceListResponse>>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetAllPricesLists_ReturnsOkWithEmptyMessage_WhenServiceReturnsEmptyList()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                dbContext.PriceLists.RemoveRange(dbContext.PriceLists);
                dbContext.SaveChanges();
            }
            var response = await _client.GetAsync($"/v1/Pricelist");
            
            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content, Is.EqualTo("There is no prices lists in database :( "));
            });
        }
    }
}
