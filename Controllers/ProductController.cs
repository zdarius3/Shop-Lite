using Microsoft.AspNetCore.Mvc;
using ShopLite.Data;
using ShopLite.Entities;
using ShopLite.Services;
using ShopLite.Interfaces;
using ShopLite.DTOs;

namespace ShopLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/all")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("/non-deleted")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNonDeletedProducts()
        {
            var products = await _productService.GetNonDeletedProductsAsync();
            return Ok(products);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById),
                new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await _productService.UpdateProductAsync(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.SoftDeleteProductAsync(id);
            return NoContent();
        }
    }
}