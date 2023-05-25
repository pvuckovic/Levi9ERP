using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Levi9.ERP.IntegrationTests.Controllers
{
    [TestFixture]
    public class DocumentControllerTests
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
                var clients = Fixture.GenerateClientData();
                dbContext.Clients.AddRange(clients);
                var products = Fixture.GenerateProductData();
                dbContext.Products.AddRange(products);
                var document = Fixture.GenerateDocumentsData();
                document.ClientId = clients.First().Id; 
                dbContext.Documents.Add(document);
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
        public async Task GetDocumentById_ReturnsOk()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/v1/Document/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DocumentResponse>(content);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.DocumentId);
        }

        [Test]
        public async Task GetDocumentById_NoDocumentFound_ReturnsNotFound()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/v1/Document/44");
            var result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("\"A document with that id doesn't exists\"", result);
        }


        [Test]
        public async Task GetDocumentById_Without_Token_ReturnsUnauthorized()
        {
            var response = await _client.GetAsync("/v1/Document/1");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task CreateDocument_ValidDocument_ReturnsCreated()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var documentRequest = new DocumentRequest
            {
                ClientId = 1,
                DocumentType = DocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Name = "A-Jacket",
                        PriceValue = 9.99f,
                        Currency = CurrencyType.USD,
                        Quantity = 2
                    }
                }
            };
            var jsonRequest = JsonConvert.SerializeObject(documentRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/v1/Document", httpContent);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task CreateDocument_InvalidClientId_ReturnsNotFound()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var documentRequest = new DocumentRequest
            {
                ClientId = 33,
                DocumentType = DocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Name = "Shirt",
                        PriceValue = 12,
                        Currency = CurrencyType.USD,
                        Quantity = 2
                    }
                }
            };
            var jsonRequest = JsonConvert.SerializeObject(documentRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/v1/Document", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("\"Client doesn't exists\"", result);
        }

        [Test]
        public async Task CreateDocument_Without_Token_ReturnsUnauthorized()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "uyftghkbvgjcjv");
            var documentRequest = new DocumentRequest
            {
                ClientId = 33,
                DocumentType = DocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 5,
                        Name = "Shirt",
                        PriceValue = 12,
                        Currency = CurrencyType.USD,
                        Quantity = 2
                    }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(documentRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/v1/Document", httpContent);

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Test]
        public async Task SearchDocuments_By_Name_Ascending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Jacket", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.ASC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderBy(p => p.DocumentType, StringComparer.Ordinal);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchDocuments_By_Name_Descending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Jacket", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.DESC };
 
            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderByDescending(p => p.DocumentType, StringComparer.Ordinal);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }
        [Test]
        public async Task SearchDocuments_By_ClientName_Ascending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Zlatko", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.ASC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderBy(p => p.DocumentType);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchDocuments_By_ClientName_Descending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Zlatko", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.DESC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderByDescending(p => p.DocumentType);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchDocuments_By_DocumentType_Ascending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "INVOICE", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.ASC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderBy(p => p.DocumentType);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchDocuments_By_DocumentType_Descending_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "INVOICE", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.DESC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);
 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
            var sortedItems = content.Items.OrderByDescending(p => p.DocumentType);
            Assert.IsTrue(content.Items.SequenceEqual(sortedItems));
        }

        [Test]
        public async Task SearchDocuments_Without_OrderBy_And_Direction_ValidParams_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Jacket" };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}");
            var result = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<SearchDocumentResponse>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<IEnumerable<DocumentResponse>>(content.Items);
            Assert.AreEqual(1, content.Page);
        }

        [Test]
        public async Task SearchDocuments_Products_Not_Exists_ReturnsNotFound()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "SomeTestProduct", OrderBy = OrderByDocumentSearch.documentType, Direction = DirectionType.ASC };

            var response = await _client.GetAsync($"/v1/Document?Page={searchParams.Page}&Name={searchParams.Name}&OrderBy={searchParams.OrderBy}&Direction={searchParams.Direction}");
            var result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("\"No documents were found that match the search parameters\"", result);
        }

        [Test]
        public async Task SyncDocuments_EmptyList_ReturnsBadRequest()
        {
            // Arrange
            // Act
            var clients = new List<DocumentSyncRequest>
            {
            };

            var jsonRequest = JsonConvert.SerializeObject(clients);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/v1/document/sync", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("\"Update failed!\"", result);
        }
    }
}
