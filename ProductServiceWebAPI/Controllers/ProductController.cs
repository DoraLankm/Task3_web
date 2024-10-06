
using Microsoft.AspNetCore.Mvc;
using ProductServiceWebAPI.Abstraction;
using Shared.Dto;

namespace ProductServiceWebAPI.Controllers
{
    [ApiController]
    [Route("product")] 
    public class ProductController: ControllerBase
    {
        private readonly IProductServices _service;

        public ProductController(IProductServices service)
        {
            _service = service;
        }


        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            var products = _service.GetProducts();
            return Ok(products);
        }

        [HttpPut("addProduct")]
        public IActionResult AddProduct([FromBody] ProductDto productDTO)
        {
            var result = _service.AddProduct(productDTO);
            return Ok(result);
        }
    }
}
