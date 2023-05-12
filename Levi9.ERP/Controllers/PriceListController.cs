using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Datas.Responses;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PricelistController : ControllerBase
    {
        private readonly IPriceListService _priceListService;
        private readonly IMapper _mapper;
        public PricelistController(IPriceListService priceListService, IMapper mapper) 
        {
            _priceListService = priceListService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            if(id <= 0)
                return BadRequest($"Invalid number({id}) of ID");

            var priceListDTO = await _priceListService.GetByIdAsync(id);

            if(priceListDTO == null) 
                return NotFound($"Nonexistent price list with ID: {id}"); 
            
            var priceListResponse = _mapper.Map<PriceListResponse>(priceListDTO);

            return Ok(priceListResponse);
        }

        [HttpGet]
        [Route("global/{globalId}")]
        public async Task<IActionResult> GetByGlobalId(Guid globalId)
        {
            var priceListDTO = await _priceListService.GetByGlobalIdAsync(globalId);

            if (priceListDTO == null)
                return NotFound($"Nonexistent price list with global ID: {globalId}");

            var priceListResponse = _mapper.Map<PriceListResponse>(priceListDTO);

            return Ok(priceListResponse);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPricesLists()
        {
            var list = await _priceListService.GetAllPricesLists();

            if (!list.Any())
                return Ok("There is no prices lists in database :( ");

            var listResponse = list.Select(p => _mapper.Map<PriceListResponse>(p));
            
            return Ok(listResponse);
        }

        [HttpPost]
        [Route("product/price")]
        public async Task<IActionResult> AddProductIntoPriceList([FromBody]PriceRequest priceRequest)
        {
            var priceProductDto = _mapper.Map<PriceProductDTO>(priceRequest);

            var newPriceProductDto = await _priceListService.AddPrice(priceProductDto);
            if (newPriceProductDto == null)
                return BadRequest();

            var priceResponse = _mapper.Map<PriceResponse>(newPriceProductDto);
            return Ok(priceResponse);
        }
        [HttpPut]
        [Route("product/price")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceRequest priceRequest)
        {
            var priceProductDto = _mapper.Map<PriceProductDTO>(priceRequest);

            var newPriceProductDto = await _priceListService.UpdatePrice(priceProductDto);
            if (newPriceProductDto == null)
                return BadRequest();

            var priceResponse = _mapper.Map<PriceResponse>(newPriceProductDto);
            return Ok(priceResponse);
        }
        [HttpGet]
        [Route("prices")]
        public async Task<IActionResult> SearchArticles([FromQuery] SearchArticleRequest searchArticleRequest)
        {
            var searchArticleDTO = _mapper.Map<SearchArticleDTO>(searchArticleRequest);

            if (searchArticleDTO.OrderBy != null && searchArticleDTO.Direction == null)
                return BadRequest("Direction is required, becuse OrderBy is selected");
            
            if(searchArticleDTO.OrderBy == null && searchArticleDTO.Direction == null)
                searchArticleDTO.Direction = DirectionType.DESC;

            var searchArticleResponse = await _priceListService.SearchArticle(searchArticleDTO);

            if (!searchArticleResponse.Any())
                return Ok("There is no articles found that match the search parameters :( ");

            return Ok(searchArticleResponse);
        }
    }
}