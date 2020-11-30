using System;
using System.Net;
using DecoratorPatternExample.DataAccessLayer;
using DecoratorPatternExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPatternExample.Controllers
{
    [Route("admin/products")]
    public class AdminProductController : ControllerBase
    {
        private readonly IAdminProductRepository _productRepository;

        public AdminProductController(IAdminProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("")]
        public IActionResult PostProduct([FromBody] Product? product)
        {
            if (string.IsNullOrEmpty(product?.Name)) return StatusCode((int) HttpStatusCode.BadRequest, "Product name should not be null or empty");
            if ((product?.Price ?? decimal.Zero) <= decimal.Zero) return StatusCode((int) HttpStatusCode.BadRequest, "Product name should not be null or empty");
            
            product.Id = Guid.NewGuid().ToString();
            string productId = _productRepository.CreateProduct(product);
            return StatusCode((int) HttpStatusCode.Created, productId);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] string id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null) return StatusCode((int) HttpStatusCode.NotFound);
            return StatusCode((int) HttpStatusCode.OK, product);
        }
    }
}