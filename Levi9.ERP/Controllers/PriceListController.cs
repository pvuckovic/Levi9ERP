using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    
    public class PricelistController : ControllerBase
    {
        private readonly IPriceListService _priceListService;
        private readonly IMapper _mapper;
        private readonly ILogger<PricelistController> _logger;
        public PricelistController(IPriceListService priceListService, IMapper mapper, ILogger<PricelistController> logger)
        {
            _priceListService = priceListService;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(Get), DateTime.UtcNow);

            if (id <= 0)
            {
                _logger.LogError("Invalid id in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", id, nameof(Get), DateTime.UtcNow);
                return BadRequest($"Invalid number({id}) of ID");
            }

            var priceListDTO = await _priceListService.GetByIdAsync(id);

            if (priceListDTO == null)
            {
                _logger.LogError("Invalid priceList id in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", id, nameof(Get), DateTime.UtcNow);
                return NotFound($"Nonexistent price list with ID: {id}");
            }

            var priceListResponse = _mapper.Map<PriceListResponse>(priceListDTO);
            _logger.LogInformation("Pricelist retrieved successfully with ID: {PricelistId} in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", id, nameof(Get), DateTime.UtcNow);
            return Ok(priceListResponse);
        }

        [HttpGet]
        [Authorize]
        [Route("global/{globalId}")]
        public async Task<IActionResult> GetByGlobalId(Guid globalId)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(GetByGlobalId), DateTime.UtcNow);
            var priceListDTO = await _priceListService.GetByGlobalIdAsync(globalId);

            if (priceListDTO == null)
            {
                _logger.LogInformation("Invalid globalid in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", nameof(GetByGlobalId), DateTime.UtcNow);
                return NotFound($"Nonexistent price list with global ID: {globalId}");
            }

            var priceListResponse = _mapper.Map<PriceListResponse>(priceListDTO);
            _logger.LogInformation("Pricelist retrieved successfully with ID: {PricelistId} in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", globalId, nameof(GetByGlobalId), DateTime.UtcNow);
            return Ok(priceListResponse);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPricesLists()
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
            var list = await _priceListService.GetAllPricesLists();

            if (!list.Any())
            {
                _logger.LogInformation("There is no prices lists in database PriceListController. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
                return Ok("There is no prices lists in database :( ");
            }

            var listResponse = list.Select(p => _mapper.Map<PriceListResponse>(p));
            _logger.LogInformation("Pricelists retrieved successfully in {FunctionName} of PriceListController. Timestamp: {Timestamp}.", nameof(GetAllPricesLists), DateTime.UtcNow);
            return Ok(listResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("product/price")]
        public async Task<IActionResult> AddProductIntoPriceList([FromBody] PriceRequest priceRequest)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(AddProductIntoPriceList), DateTime.UtcNow);
            var priceProductDto = _mapper.Map<PriceProductDTO>(priceRequest);

            var newPriceProductDto = await _priceListService.AddPrice(priceProductDto);
            if (newPriceProductDto == null)
            {
                _logger.LogInformation("Bad request in PriceListController. Timestamp: {Timestamp}.", nameof(AddProductIntoPriceList), DateTime.UtcNow);
                return BadRequest();
            }

            var priceResponse = _mapper.Map<PriceResponse>(newPriceProductDto);
            _logger.LogInformation("Successfully added product into pricelist in PriceListController Timestamp: {Timestamp}.", DateTime.UtcNow);
            return Ok(priceResponse);
        }
        [HttpPut]
        [Authorize]
        [Route("product/price")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceRequest priceRequest)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);

            var priceProductDto = _mapper.Map<PriceProductDTO>(priceRequest);

            var newPriceProductDto = await _priceListService.UpdatePrice(priceProductDto);
            if (newPriceProductDto == null)
            {
                _logger.LogInformation("Bad request in PriceListController. Timestamp: {Timestamp}.", nameof(UpdatePrice), DateTime.UtcNow);
                return BadRequest();
            }

            var priceResponse = _mapper.Map<PriceResponse>(newPriceProductDto);
            _logger.LogInformation("Successfully updated price in PriceListController Timestamp: {Timestamp}.", DateTime.UtcNow);
            return Ok(priceResponse);
        }
        [HttpGet]
        [Authorize]
        [Route("prices")]
        public async Task<IActionResult> SearchArticles([FromQuery] SearchArticleRequest searchArticleRequest)
        {
            _logger.LogInformation("Entering {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(SearchArticles), DateTime.UtcNow);
            var searchArticleDTO = _mapper.Map<SearchArticleDTO>(searchArticleRequest);

            if (searchArticleDTO.OrderBy != null && searchArticleDTO.Direction == null)
            {
                _logger.LogInformation("Bad request in {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(SearchArticles), DateTime.UtcNow);
                return BadRequest("Direction is required, because OrderBy is selected");
            }

            if (searchArticleDTO.OrderBy == null && searchArticleDTO.Direction == null)
                searchArticleDTO.Direction = DirectionType.DESC;

            var priceListArticleDTOs = await _priceListService.SearchArticle(searchArticleDTO);

            if (!priceListArticleDTOs.Any())
            {
                _logger.LogInformation("There is no articles found in {FunctionName} in PriceListController. Timestamp: {Timestamp}.", nameof(SearchArticles), DateTime.UtcNow);
                return Ok("There is no articles found that match the search parameters :( ");
            }

            var searchArticleResponse = new SearchArticleResponse
            {
                PricelistArticles = _mapper.Map<List<PriceListArticleResponse>>(priceListArticleDTOs),
                Page = searchArticleRequest.PageId
            };
            _logger.LogInformation("Successful search in PriceListController Timestamp: {Timestamp}.", DateTime.UtcNow);
            return Ok(searchArticleResponse);
        }
    }
}