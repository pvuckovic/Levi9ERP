using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Models.DTO.DocumentDto;
using Levi9.ERP.Domain.Repositories;
using Levi9.ERP.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Services
{
    public class DocumentServiceTests
    {
        private Mock<IDocumentRepository> _documentMockRepository;
        private Mock<IClientRepository> _clientMockRepository;
        private Mock<IProductRepository> _productMockRepository;
        private DocumentService _documentService;
        private Mock<ILogger<DocumentService>> _loggerMock;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _documentMockRepository = new Mock<IDocumentRepository>();
            _loggerMock = new Mock<ILogger<DocumentService>>();
            _clientMockRepository = new Mock<IClientRepository>();
            _productMockRepository = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _documentService = new DocumentService(_documentMockRepository.Object, _clientMockRepository.Object, _productMockRepository.Object, _loggerMock.Object, _mapper.Object);
        }

        [Test]
        public async Task CreateDocument_ValidDocument_ReturnsDTO()
        {
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = DocumentType.INVOICE,
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
                DocumentType = DocumentType.INVOICE,
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
                DocumentType = DocumentType.INVOICE,
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
                OrderBy = OrderByDocumentSearch.documentType,
                Direction = DirectionType.ASC
            };
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = DocumentType.INVOICE,
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
                searchParams.Name, searchParams.Page, searchParams.OrderBy.ToString(), searchParams.Direction.ToString()))
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
                OrderBy = OrderByDocumentSearch.documentType,
                Direction = DirectionType.ASC
            };
            var documentDTO = new DocumentDTO
            {
                ClientId = 1,
                DocumentType = DocumentType.INVOICE,
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
                searchParams.Name, searchParams.Page, searchParams.OrderBy.ToString(), searchParams.Direction.ToString()))
                .ReturnsAsync(new List<DocumentDTO> { });

            var result = await _documentService.GetDocumentsByParameters(searchParams);

            Assert.That(result, Is.InstanceOf<List<DocumentDTO>>());
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task SyncDocuments_DocumentExists_UpdatesDocumentAndReturnsLastUpdate()
        {
            // Arrange
            var documents = new List<DocumentSyncDTO>
            {
                new DocumentSyncDTO
                {
                    LastUpdate = "2023-05-24",
                    GlobalId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    Items = new List<DocumentItemSyncDTO>
                    {
                        new DocumentItemSyncDTO
                        {
                            ProductId = Guid.NewGuid(),

                        },
                        new DocumentItemSyncDTO
                        {
                            ProductId = Guid.NewGuid(),

                        }
                    }
                }
            };

            var document = new DocumentDTO
            {
                LastUpdate = "2023-05-24",
                GlobalId = Guid.NewGuid(),
                Id = 0,
                Items = new List<DocumentItemDTO>
                {
                    new DocumentItemDTO
                    {
                        ProductId = 2
                    },
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                    },
                }
            };

            var clientId = 123; // Example client ID
            var productIds = new List<int> { 456, 789 }; // Example product IDs
            var lastUpdate = "2023-05-24"; // Example last update value

            _mapper.Setup(x => x.Map<DocumentDTO>(It.IsAny<DocumentSyncDTO>()))
                .Returns(document);

            _clientMockRepository.Setup(x => x.GetClientIdFromClientGlobalId(It.IsAny<Guid>()))
                .ReturnsAsync(clientId);

            _productMockRepository.SetupSequence(x => x.GetProductIdFromProductGlobalId(It.IsAny<Guid>()))
                .ReturnsAsync(productIds[0])
                .ReturnsAsync(productIds[1]);

            _documentMockRepository.Setup(x => x.DoesDocumentByGlobalIdExists(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            // Act
            var result = await _documentService.SyncDocuments(documents);

            // Assert
            Assert.AreEqual(lastUpdate, result);
            _documentMockRepository.Verify(x => x.UpdateDocument(document), Times.Once);
        }

        [Test]
        public async Task SyncDocuments_DocumentDoesNotExist_AddsDocumentAndReturnsLastUpdate()
        {
            // Arrange
            var documents = new List<DocumentSyncDTO>
            {
                new DocumentSyncDTO
                {
                    LastUpdate = "2023-05-24",
                    GlobalId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    Items = new List<DocumentItemSyncDTO>
                    {
                        new DocumentItemSyncDTO
                        {
                            ProductId = Guid.NewGuid(),

                        },
                        new DocumentItemSyncDTO
                        {
                            ProductId = Guid.NewGuid(),

                        }
                    }
                }
            };

            var document = new DocumentDTO
            {
                LastUpdate = "2023-05-24",
                GlobalId = Guid.NewGuid(),
                Id = 0,
                Items = new List<DocumentItemDTO>
                {
                    new DocumentItemDTO
                    {
                        ProductId = 2
                    },
                    new DocumentItemDTO
                    {
                        ProductId = 1,
                    },
                }
            };

            var clientId = 123; // Example client ID
            var productIds = new List<int> { 456, 789 }; // Example product IDs
            var lastUpdate = "2023-05-24"; // Example last update value
            var newGlobalId = Guid.NewGuid(); // Example new global ID for the document

            _mapper.Setup(x => x.Map<DocumentDTO>(It.IsAny<DocumentSyncDTO>()))
                .Returns(document);

            _clientMockRepository.Setup(x => x.GetClientIdFromClientGlobalId(It.IsAny<Guid>()))
                .ReturnsAsync(clientId);

            _productMockRepository.SetupSequence(x => x.GetProductIdFromProductGlobalId(It.IsAny<Guid>()))
                .ReturnsAsync(productIds[0])
                .ReturnsAsync(productIds[1]);

            _documentMockRepository.Setup(x => x.DoesDocumentByGlobalIdExists(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            // Act
            var result = await _documentService.SyncDocuments(documents);

            // Assert
            Assert.AreEqual(lastUpdate, result);
            _documentMockRepository.Verify(x => x.AddDocument(document), Times.Once);
            Assert.AreEqual(newGlobalId, document.GlobalId);
        }
    }
}
