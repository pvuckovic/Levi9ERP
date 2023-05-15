using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Levi9.ERP.IntegrationTests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {

        private TestingWebAppFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new TestingWebAppFactory<Program>();
            _client = _factory.CreateClient();
        }


        [Test]
        public async Task CreateProductAsync_ValidProduct_ReturnsOk()
        {
            // Arrange
            var productRequest = new ProductRequest { Name = "Test Product", ImageUrl = "slika.png" };
            // Act
            var response = await _client.PostAsJsonAsync("/v1/Product", productRequest);
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<ProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Test Product", content.Name);
            Assert.AreEqual("slika.png", content.ImageUrl);
        }

        [Test]
        public async Task CreateProductAsync_ExistNameProduct_ReturnsBadRequest()
        {
            // Arrange
            var productRequest = new ProductRequest { Name = "Shirt", ImageUrl = "slika.png" };
            // Act
            var response = await _client.PostAsJsonAsync("/v1/Product", productRequest);
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("A product with the same name already exists.", result);
        }

        [Test]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            // Act
            var response = await _client.GetAsync($"/v1/Product/{id}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<ProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(id, content.Id);
            Assert.AreEqual("Shirt", content.Name);
        }

        [Test]
        public async Task GetById_WithNegativeId_ReturnsNotFound()
        {
            // Arrange
            var id = -1;
            // Act
            var response = await _client.GetAsync($"/v1/Product/{id}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Id is null or negative number", result);
        }

        [Test]
        public async Task GetById_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var id = 492342;
            // Act
            var response = await _client.GetAsync($"/v1/Product/{id}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("A product with the same id doesn't exists.", result);
        }

        [Test]
        public async Task GetByGlobalId_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = "494ad824-8ea2-47c3-938f-2de7a43db41a";
            // Act
            var response = await _client.GetAsync($"/v1/Product/Global/{id}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<ProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Shirt", content.Name);
        }

        [Test]
        public async Task GetByGlobalId_WithNonexistentId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var response = await _client.GetAsync($"/v1/Product/Global/{id}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("A product with that id doesn't exists.", result);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

    }
}