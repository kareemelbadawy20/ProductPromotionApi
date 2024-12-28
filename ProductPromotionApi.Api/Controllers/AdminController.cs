using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPromotionApi.Api.DTOs;
using ProductPromotionApi.Application.Interfaces;
using ProductPromotionApi.Core.Entities;

namespace ProductPromotionApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;

        public AdminController(IProductService productService, IPromotionService promotionService, IMapper mapper)
        {
            _productService = productService;
            _promotionService = promotionService;
            _mapper = mapper;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDto);
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _mapper.Map<ProductDto>(product));
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("promotions")]
        public async Task<IActionResult> GetPromotions()
        {
            var promotions = await _promotionService.GetPromotionsAsync();
            var promotionDtos = _mapper.Map<IEnumerable<PromotionDto>>(promotions);
            return Ok(promotionDtos);
        }

        [HttpGet("promotions/{id}")]
        public async Task<IActionResult> GetPromotion(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
                return NotFound();

            var promotionDto = _mapper.Map<PromotionDto>(promotion);
            return Ok(promotionDto);
        }

        [HttpPost("promotions")]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionDto promotionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promotion = _mapper.Map<Promotion>(promotionDto);
            await _promotionService.AddPromotionAsync(promotion);
            return CreatedAtAction(nameof(GetPromotion), new { id = promotion.Id }, _mapper.Map<PromotionDto>(promotion));
        }

        [HttpPut("promotions/{id}")]
        public async Task<IActionResult> UpdatePromotion(int id, [FromBody] UpdatePromotionDto promotionDto)
        {
            if (id != promotionDto.Id)
                return BadRequest();

            var promotion = _mapper.Map<Promotion>(promotionDto);
            await _promotionService.UpdatePromotionAsync(promotion);
            return NoContent();
        }

        [HttpDelete("promotions/{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await _promotionService.DeletePromotionAsync(id);
            return NoContent();
        }
        [HttpPost("products/apply-promotion")]
        public async Task<IActionResult> ApplyPromotion([FromBody] ApplyPromotionDto applyPromotionDto)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productService.ApplyPromotionAsync(applyPromotionDto.ProductId, applyPromotionDto.PromotionId);
            return NoContent(); 
        }
    }

}
