using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync([FromBody] string name)
        {
            try
            {
                var existingProduct = await _productService.GetProductByName(name);
                if (existingProduct != null) return BadRequest("A product with the same name already exists.");
                var product = await _productService.CreateProductAsync(name);
                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
