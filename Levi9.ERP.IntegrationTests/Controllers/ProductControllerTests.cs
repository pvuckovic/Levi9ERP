using Levi9.ERP.Data.Requests;
using Levi9.ERP.Data.Responses;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Levi9.ERP.IntegrationTests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {

        private TestingWebAppFactory<Program> _factory;
        private HttpClient _client;
        private DataBaseContext _dataBaseContext;

        [SetUp]
        public void Setup()
        {
            _factory = new TestingWebAppFactory<Program>();
            _client = _factory.CreateClient();
            string token = Fixture.GenerateJwt();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Products.AddRange(Fixture.GenerateProductData());
                dbContext.SaveChanges();
            }
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
            var productRequest = new ProductRequest { Name = "A-Jacket", ImageUrl = "slika.png" };
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
            Assert.AreEqual("A-Jacket", content.Name);
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
            var id = "80ee1a50-522a-4813-842f-0a0766a4d71e";
            // Act
            var response = await _client.GetAsync($"/v1/Product/Global/{id}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<ProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("A-Jacket", content.Name);
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

        [Test]
        public async Task SearchProducts_By_Name_Ascending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "name", Direction = "asc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderBy(p => p.Name, StringComparer.Ordinal);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_By_Name_Descending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "name", Direction = "dsc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderByDescending(p => p.Name, StringComparer.Ordinal);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_By_Id_Ascending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "id", Direction = "asc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderBy(p => p.Id);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_By_Id_Descending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "id", Direction = "dsc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderByDescending(p => p.Id);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_By_AvailableQuantity_Ascending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "availableQuantity", Direction = "asc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderBy(p => p.AvailableQuantity);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_By_AvailableQuantity_Descending_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "availableQuantity", Direction = "dsc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
            var sortedItems = content.Items.OrderByDescending(p => p.AvailableQuantity);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchProducts_Without_OrderBy_And_Direction_ValidParams_ReturnsOkResult()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchProductResponse>(result);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<ProductResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            Assert.IsTrue(content.Items.All(p => p.Name.Contains("Jacket")));
        }

        [Test]
        public async Task SearchProducts_Without_Direction_ReturnsBadRequest()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Jacket", OrderBy = "name" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("If 'orderBy' is not empty, you must enter 'direction'!", result);
        }

        [Test]
        public async Task SearchProducts_With_Negative_Page_Number_ReturnsBadRequest()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = -1, Name = "Jacket", OrderBy = "name" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Page must be greater than 0.", result);
        }

        [Test]
        public async Task SearchProducts_Products_Not_Exists_ReturnsNotFound()
        {
            // Arrange
            var searchParams = new SearchProductRequest { Page = 1, Name = "Nema", OrderBy = "name", Direction = "dsc" };
            // Act
            var response = await _client.GetAsync($"/v1/Product/Search?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("No products were found that match the search parameters.", result);
        }
        [Test]
        public async Task GetAllProducts_ReturnsOkWithMappedList_WhenServiceReturnsNonEmptyList()
        {
            var response = await _client.GetAsync("/v1/Product/sync/133288706851213387");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ProductResponse>>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result, Is.Not.Null);
            });
        }
        [Test]
        public async Task GetAllProducts_ReturnsOkWithEmptyList_WhenServiceReturnsEmptyList()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.SaveChanges();
            }
            var response = await _client.GetAsync("/v1/Product/sync/933288706851213387");

            var content = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content, Is.EqualTo("[]"));
            });
        }
        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

    }
}