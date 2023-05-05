using AutoMapper;
using Levi9.ERP.Data.Response;
using Levi9.ERP.Domain.Contracts;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceListService _priceListService;
        private readonly IMapper _mapper;
        public PriceListController(IPriceListService priceListService, IMapper mapper) 
        {
            _priceListService = priceListService;
            _mapper = mapper;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id) 
        {
            try
            {

                var priceDTO = await _priceListService.GetByIdAsync(id);
                var priceListResponse = _mapper.Map<PriceListResponse>(priceDTO);
                return Ok(priceListResponse);
            }
            catch(InvalidOperationException)
            {
                return BadRequest($"Non existent price list with ID: {id}");
            }
            catch(ArgumentException)
            {
                return BadRequest($"Invalid number({id}) of ID");
            }
        }
    }
}
