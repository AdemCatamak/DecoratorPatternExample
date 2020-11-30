using System.Collections.Generic;
using System.Linq;
using System.Net;
using DecoratorPatternExample.DataAccessLayer;
using DecoratorPatternExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPatternExample.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] string id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null) return StatusCode((int) HttpStatusCode.NotFound);
            return StatusCode((int) HttpStatusCode.OK, product);
        }

        [HttpGet("")]
        public IActionResult GetProducts([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            IEnumerable<Product> products = _productRepository.GetProducts(skip, take);
            if (!products.Any()) return StatusCode((int) HttpStatusCode.NotFound);
            return StatusCode((int) HttpStatusCode.OK, products);
        }
    }
}