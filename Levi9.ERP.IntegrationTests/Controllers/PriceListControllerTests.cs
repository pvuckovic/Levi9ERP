using Levi9.ERP.Controllers;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

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
                Assert.That(content, Is.EqualTo($"Nonexistent price list with ID: {id}"));
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
                Assert.That(content, Is.EqualTo($"Invalid number({id}) of ID"));
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
            var response = await _client.GetAsync("/v1/Pricelist");

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
            var response = await _client.GetAsync("/v1/Pricelist");
            
            var content = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content, Is.EqualTo("There is no prices lists in database :( "));
            });
        }
        [Test]
        public async Task AddProductIntoPriceList_ReturnsOkWithPriceResponse_WhenServiceReturnsValidPriceDto()
        {
            PriceRequest priceRequest = new PriceRequest()
            {
                PriceListId = 3,
                ProductId = 1,
                Price = 12,
                Currency = CurrencyType.RSD
            };
            var response = await _client.PostAsync("/v1/Pricelist/product/price",JsonContent.Create(priceRequest));

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PriceResponse>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.ProductId, Is.EqualTo(priceRequest.ProductId));
                Assert.That(result.PriceListId, Is.EqualTo(priceRequest.PriceListId));
                Assert.That(result.Price, Is.EqualTo(priceRequest.Price));
                Assert.That(result.Currency, Is.EqualTo(priceRequest.Currency));
            });
        }
        [Test]
        public async Task AddProductIntoPriceList_ReturnsBadRequest_WhenServiceReturnsNullPriceDto()
        {
            var priceRequest = new PriceRequest
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = CurrencyType.USD
            };

            var response = await _client.PostAsync("/v1/Pricelist/product/price", JsonContent.Create(priceRequest));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task UpdatePrice_ReturnsNewPriceProductDto_WhenServiceUpdatesPrice()
        {
            var priceRequest = new PriceRequest
            {
                PriceListId = 1,
                ProductId = 1,
                Price = 9.99f,
                Currency = CurrencyType.USD
            };

            var response = await _client.PutAsync("/v1/Pricelist/product/price", JsonContent.Create(priceRequest));
            
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PriceResponse>(content);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.ProductId, Is.EqualTo(priceRequest.ProductId));
                Assert.That(result.PriceListId, Is.EqualTo(priceRequest.PriceListId));
                Assert.That(result.Price, Is.EqualTo(priceRequest.Price));
                Assert.That(result.Currency, Is.EqualTo(priceRequest.Currency));
            });
        }
        [Test]
        public async Task UpdatePrice_ReturnsBadRequest_WhenServiceReturnsNullPriceDto()
        {
            var priceRequest = new PriceRequest
            {
                PriceListId = 4,
                ProductId = 4,
                Price = 9.99f,
                Currency = CurrencyType.USD
            };

            var response = await _client.PutAsync("/v1/Pricelist/product/price", JsonContent.Create(priceRequest));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task SearchArticles_ValidRequest_ReturnsOkWithResults()
        {
            var searchRequest = new SearchArticleRequest
            {
                PageId = 1,
                SearchString = "price",
                OrderBy = OrderByArticleType.ProductId,
                Direction = DirectionType.ASC
            };
            var search = "PageId=1&SearchString=price&OrderBy=ProductId&Direction=ASC";
            var response = await _client.GetAsync($"/v1/Pricelist/prices?{search}");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SearchArticleResponse>(content);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.Page, Is.EqualTo(searchRequest.PageId));
                Assert.That(result.PricelistArticles, Has.Count.AtMost(1));
            });
        }

        [Test]
        public async Task SearchArticles_NoResults_ReturnsOkWithMessage()
        {
            var search = "PageId=1&SearchString=nonexistent";
            var response = await _client.GetAsync($"/v1/Pricelist/prices?{search}");

            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content, Is.EqualTo("There is no articles found that match the search parameters :( "));
            });
        }
        [Test]
        public async Task SearchArticles_InvalidRequest_MissingDirection_ReturnsBadRequest()
        {
            var searchRequest = new SearchArticleRequest
            {
                PageId = 1,
                SearchString = "example",
                OrderBy = OrderByArticleType.ProductId,
                Direction = null
            };

            var search = "PageId=1&SearchString=nonexistent&OrderBy=ProductId";
            var response = await _client.GetAsync($"/v1/Pricelist/prices?{search}");

            var content = await response.Content.ReadAsStringAsync();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(content, Is.EqualTo("Direction is required, because OrderBy is selected"));
        }
    }
}
