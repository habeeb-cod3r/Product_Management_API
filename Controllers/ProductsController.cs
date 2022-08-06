using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Product_Management_API.Models;
using Product_Management_API.Repository;

namespace Product_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //Route to get all products
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        //Route to get a specific product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //Route to create a new product
        [HttpPost("")]
        public async Task<IActionResult> AddNewProduct([FromBody]ProductModel productModel)
        {
            var id = await _productRepository.AddProductAsync(productModel);
            return CreatedAtAction(nameof(GetProductById), new
            {
                id = id, controller = "products"
            }, id);
        }

        //Route to update a product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel productModel, [FromRoute]int id)
        {
            await _productRepository.UpdateProductAsync(id, productModel);
            return Ok();
        }

        //Route to update specific information of a product
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProductPatch([FromBody] JsonPatchDocument productModel, [FromRoute] int id)
        {
            await _productRepository.UpdateProductPatchAsync(id, productModel);
            return Ok();
        }

        [Authorize(Roles = UserRoles.Admin)]
        //Route to delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return Ok();
        }

        //Route to view disabled products
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("disabled-products")]
        public async Task<IActionResult> GetDisabledProducts()
        {
            var disabledProducts = await _productRepository.GetAllDisabledProductsAsync();
            return Ok(disabledProducts);
        }

        //route to get the sum of products since last week
        [HttpGet]
        [Route("get-sum")]
        public async Task<IActionResult> GetSumOfProducts()
        {
            var SumOfProductsWithinOneWeek = await _productRepository.GetSumOfProductsWithinOneWeekAsync();
            return Ok(SumOfProductsWithinOneWeek);
        }
    }
}
