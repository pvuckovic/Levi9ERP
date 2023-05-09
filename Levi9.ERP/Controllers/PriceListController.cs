using AutoMapper;
using Levi9.ERP.Datas.Responses;
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
    }
}