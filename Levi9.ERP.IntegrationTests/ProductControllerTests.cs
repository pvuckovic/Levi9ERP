using System.Net;

namespace Levi9.ERP.IntegrationTests
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
            public async Task HealthCheck_ReturnsHealthy()
            {
                // Arrange

                // Act
                var response = await _client.GetAsync("/api/HealthCheck");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("Healthy", responseContent);
            }

            [TearDown]
            public void TearDown()
            {
                _client.Dispose();
                _factory.Dispose();
            }
        
    }
}