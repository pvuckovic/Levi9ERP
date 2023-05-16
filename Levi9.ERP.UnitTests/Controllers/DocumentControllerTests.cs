using AutoMapper;
using Levi9.ERP.Controllers;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Mappers;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using NUnit.Framework;

namespace Levi9.ERP.UnitTests.Controllers
{
    [TestFixture]
    public class DocumentControllerTests
    {

        private Mock<IDocumentService> _mockDocumentService;
        private Mock<IClientService> _mockClientService;
        private Mock<IProductService> _mockProductService;
        private IMapper _mapper;
        private Mock<IUrlHelper> _urlHelperMock;
        private DocumentController _documentController;

        [SetUp]
        public void SetUp()
        {
            _mockDocumentService = new Mock<IDocumentService>();
            _mockClientService = new Mock<IClientService>();
            _mockProductService = new Mock<IProductService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DocumentProfiles());
                cfg.AddProfile(new ClientProfiles());
            }).CreateMapper();
            _urlHelperMock = new Mock<IUrlHelper>();
            _documentController = new DocumentController(_mockDocumentService.Object, _mockClientService.Object, _mockProductService.Object, _mapper, _urlHelperMock.Object);
            _documentController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public async Task CreatedDocumentReturns201Created()
        {
            var documentRequest = new DocumentRequest
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
            var documentItemDTO = new ProductDTO
            {
                Id = 1,
            };

            _mockClientService.Setup(x => x.GetClientById(documentRequest.ClientId)).ReturnsAsync(client);
            _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(documentItemDTO);

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

            _mockDocumentService.Setup(x => x.CreateDocument(It.IsAny<DocumentDTO>())).ReturnsAsync(documentDto);
            _urlHelperMock.Setup(x => x.Action(It.Is<UrlActionContext>(uac =>
                                                    uac.Action == "CreateDocument" &&
                                                    uac.Controller == "Document")))
                                                    .Returns("callbackUrl");

            var result = await _documentController.CreateDocument(documentRequest);

            Assert.IsInstanceOf<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(documentDto.DocumentType.ToString(), ((DocumentResponse)createdResult.Value).DocumentType);
            Assert.AreEqual(documentDto.ClientId, ((DocumentResponse)createdResult.Value).ClientId);
            Assert.AreEqual(documentDto.Items.Count, ((DocumentResponse)createdResult.Value).Items.Count);

        }

        [Test]
        public async Task CreateDocument_ClientId_Not_Exists_ReturnsNotFound()
        {
            var documentRequest = new DocumentRequest
            {
                ClientId = 7,
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
                Id = 77
            };
            var documentItemDTO = new ProductDTO
            {
                Id = 1
            };

            _mockClientService.Setup(x => x.GetClientById(documentRequest.ClientId)).ReturnsAsync((ClientDTO)null);
            _mockProductService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(documentItemDTO);

            var documentDto = new DocumentDTO
            {
                Id = 2,
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

            _mockDocumentService.Setup(x => x.CreateDocument(It.IsAny<DocumentDTO>())).ReturnsAsync(documentDto);
            _urlHelperMock.Setup(x => x.Action(It.Is<UrlActionContext>(uac =>
                                                    uac.Action == "CreateDocument" &&
                                                    uac.Controller == "Document")))
                                                    .Returns("callbackUrl");

            var result = await _documentController.CreateDocument(documentRequest);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var createdResult = (NotFoundObjectResult)result;
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [Test]
        public async Task GetById_WithValidId_ReturnsOk()
        {
            var id = 1;
            var document = new DocumentDTO
            {
                Id = id,
                GlobalId = Guid.NewGuid(),
                LastUpdate = DateTime.Now.ToFileTimeUtc().ToString()
            };
            var expectedDocumentResponse = new DocumentResponse
            {
                DocumentId = document.Id,
                GlobalId = document.GlobalId,
                LastUpdate = document.LastUpdate
            };
            _mockDocumentService.Setup(s => s.GetDocumentById(id)).ReturnsAsync(document);

            var result = await _documentController.GetById(id);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public async Task GetById_With_Invalid_Id_ReturnsNotFound()
        {
            int id = 5;
            _mockDocumentService.Setup(service => service.GetDocumentById(id)).ReturnsAsync((DocumentDTO)null);

            var result = await _documentController.GetById(id);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("A document with that id doesn't exists", notFoundResult.Value);
        }

        [Test]
        public async Task SearchDocuments_With_Valid_Query_Params_ReturnsOkResult()
        {
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "Shirt" };
            var documents = new List<DocumentDTO>
            {
                new DocumentDTO
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
                },
                new DocumentDTO
                {
                    Id = 3,
                    ClientId = 1,
                    DocumentType = DocumentType.INVOICE,
                    Items = new List<DocumentItemDTO>()
                    {
                        new DocumentItemDTO
                        {
                            ProductId = 2,
                            Quantity = 2
                        }
                    }
                }
            };
            _mockDocumentService.Setup(s => s.GetDocumentsByParameters(It.IsAny<SearchDocumentDTO>()))
               .ReturnsAsync(documents);

            var result = await _documentController.SearchDocuments(searchParams);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task SearchDocuments_With_InValid_Query_Params_ReturnsNotFound()
        {
            var searchParams = new SearchDocumentRequest { Page = 1, Name = "name" };
            var documents = new List<DocumentDTO>
            {
                new DocumentDTO
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
                },
                new DocumentDTO
                {
                    Id = 3,
                    ClientId = 1,
                    DocumentType = DocumentType.INVOICE,
                    Items = new List<DocumentItemDTO>()
                    {
                        new DocumentItemDTO
                        {
                            ProductId = 2,
                            Quantity = 2
                        }
                    }
                }
            };
            _mockDocumentService.Setup(s => s.GetDocumentsByParameters(It.IsAny<SearchDocumentDTO>()))
               .ReturnsAsync(new List<DocumentDTO>{ });

            var result = await _documentController.SearchDocuments(searchParams);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("No documents were found that match the search parameters", notFoundResult.Value);
        }
    }
}
