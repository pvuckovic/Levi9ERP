using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    public class DocumentServiceTests
    {
        private Mock<IDocumentRepository> _documentMockRepository;
        private Mock<IClientService> _clientMockRepository;
        private Mock<IProductRepository> _productMockRepository;
        private DocumentService _documentService;

        [SetUp]
        public void SetUp()
        {
            _documentMockRepository = new Mock<IDocumentRepository>();
            _documentService = new DocumentService(_documentMockRepository.Object);
            _clientMockRepository = new Mock<IClientService>();
            _productMockRepository = new Mock<IProductRepository>();
        }

        [Test]
        public async Task CreateDocument_ValidDocument_ReturnsDTO()
        {
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = EnumDocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            var client = new ClientDTO
            {
                Id = 1,
            };
            var documentItemDTO = new Product()
            {
                Id = 1,
            };
            _clientMockRepository.Setup(x => x.GetClientById(documentDTO.ClientId)).ReturnsAsync(client);
            _productMockRepository.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(documentItemDTO);
            var documentDto = new DocumentDTO
            {
                Id = 2,
                ClientId = 1,
                DocumentType = EnumDocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            _documentMockRepository.Setup(x => x.AddDocument(documentDTO)).ReturnsAsync(documentDto);

            var result = await _documentService.CreateDocument(documentDTO);

            Assert.IsNotNull(result);
        }

        [Test]

        public async Task CreateDocument_With_Invalid_ClientId_Returns_Null()
        {
            var documentDTO = new DocumentDTO
            {
                ClientId = 55,
                DocumentType = EnumDocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            var client = new ClientDTO
            {
                Id = 0,
            };
            var documentItemDTO = new Product()
            {
                Id = 1,
            };
            _clientMockRepository.Setup(x => x.GetClientById(documentDTO.ClientId)).ReturnsAsync(client);
            _productMockRepository.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(documentItemDTO);
            DocumentDTO documentDto = null;
            _documentMockRepository.Setup(x => x.AddDocument(documentDTO)).ReturnsAsync(documentDto);

            var result = await _documentService.CreateDocument(documentDTO);

            Assert.Null(result);
        }

        [Test]
        public async Task GetDocumentById_WhenDocumentExists_ReturnsDocument()
        {
            int documentId = 1;
            DocumentDTO document = new DocumentDTO { Id = documentId };
            DocumentDTO expectedDocumentDto = new DocumentDTO { Id = documentId };
            _documentMockRepository.Setup(repo => repo.GetDocumentById(documentId)).ReturnsAsync(document);

            DocumentDTO actualDocumentDto = await _documentService.GetDocumentById(documentId);

            Assert.IsInstanceOf<DocumentDTO>(actualDocumentDto);
            Assert.AreEqual(expectedDocumentDto.Id, actualDocumentDto.Id);
        }

        [Test]
        public async Task GetDocumentById_WhenDocumentDoesNotExist_ReturnsNull()
        {
            int documentId = 1;
            _documentMockRepository.Setup(repo => repo.GetDocumentById(documentId)).ReturnsAsync((DocumentDTO)null);

            var result = await _documentService.GetDocumentById(documentId);

            Assert.Null(result);
        }
        [Test]
        public async Task GetDocumentsByParameters_ReturnsExpectedResult()
        {
            var searchParams = new SearchDocumentDTO
            {
                Name = "Shirt",
                Page = 1,
                OrderBy = "documentType",
                Direction = "asc"
            };
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = EnumDocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            var client = new ClientDTO
            {
                Id = 1,
            };
            var documentItemDTO = new Product()
            {
                Id = 1,
            };
            _documentMockRepository.Setup(x => x.GetDocumentsByParameters(
                searchParams.Name, searchParams.Page, searchParams.OrderBy, searchParams.Direction))
                .ReturnsAsync(new List<DocumentDTO> { documentDTO });

            var result = await _documentService.GetDocumentsByParameters(searchParams);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var documentDto = result.First();
            Assert.AreEqual(documentDTO.ClientId, documentDto.ClientId);
        }
        [Test]
        public async Task GetDocuments_ByInvalid_Parameters_ReturnsNotFound()
        {
            var searchParams = new SearchDocumentDTO
            {
                Name = "fffffffffff",
                Page = 1,
                OrderBy = "documentType",
                Direction = "asc"
            };
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = EnumDocumentType.INVOICE,
                Items = new List<DocumentItemDTO>()
                {
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            var client = new ClientDTO
            {
                Id = 1,
            };
            var documentItemDTO = new Product()
            {
                Id = 1,
            };
            _documentMockRepository.Setup(x => x.GetDocumentsByParameters(
                searchParams.Name, searchParams.Page, searchParams.OrderBy, searchParams.Direction))
                .ReturnsAsync(new List<DocumentDTO> { });

            var result = await _documentService.GetDocumentsByParameters(searchParams);

            Assert.That(result, Is.InstanceOf<List<DocumentDTO>>());
            Assert.IsEmpty(result);
        }
    }
}
