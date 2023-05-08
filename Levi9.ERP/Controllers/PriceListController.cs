using AutoMapper;
using Levi9.ERP.Data.Response;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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
            /*
            var priceDTO = await _priceListService.GetByIdAsync(id);

            if(priceDTO == null) 
                return NotFound($"Nonexistent price list with ID: {id}"); 
            
            var priceListResponse = _mapper.Map<PriceListResponse>(priceDTO);

            return Ok(priceListResponse);
            */
            try
            {
                var priceDTO = await _priceListService.GetByIdAsync(id);
                var priceListResponse = _mapper.Map<PriceListResponse>(priceDTO);
                return Ok(priceListResponse);
            }
            catch(InvalidOperationException)
            {
                return NotFound($"Nonexistent price list with ID: {id}");
            }
            catch(ArgumentException)
            {
                return BadRequest($"Invalid number({id}) of ID");
            }
        }
    }
}
