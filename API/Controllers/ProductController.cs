using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController(ILogger<ProductController> logger, IProductRepository productRepository) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;

        private readonly IProductRepository _productRepository = productRepository;

        [HttpPost("product")]
        public async Task<ActionResult<Result>> CreateProduct(ProductDto request)
        {
            try {
                var response = await _productRepository.CreateProduct(request);
                return response;
            }
            catch(Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("Creation failed");
            }
        }

        [HttpGet("products/{status}")]
        public async Task<ActionResult<Result>> GetAll(int status)
        {
            try
            {
                var response = await _productRepository.GetAllProducts(status);
                return response;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("GetAll failed");
            }
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<Result>> GetProduct(int id)
        {
            try
            {
                var response = await _productRepository.GetProduct(id);
                return response;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("GetProduct failed");
            }
        }

        [HttpPut("product/:id")]
        public async Task<ActionResult<Result>> UpdateProduct([FromBody] ProductDto request, int id)
        {
            try
            {
                var response = await _productRepository.UpdateProduct(id, request);
                return response;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("UpdateProduct failed");
            }
        }

        [HttpDelete("product/:id")]
        public async Task<ActionResult<Result>> DeleteProduct(int id)
        {
            try
            {
                var response = await _productRepository.DeleteProduct(id);
                return response;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return Result.Failure("DeleteProduct failed");
            }
        }
    }
}
