using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPromotionApi.Api.DTOs;
using ProductPromotionApi.Application.Interfaces;

namespace ProductPromotionApi.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class MobileController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;

        public MobileController(IProductService productService, IPromotionService promotionService, IMapper mapper)
        {
            _productService = productService;
            _promotionService = promotionService;
            _mapper = mapper;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(paginatedProducts);

            return Ok(productDtos);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var products = await _productService.GetProductsAsync();
            var featuredProducts = products.Take(5); 
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(featuredProducts);

            return Ok(productDtos);
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewProducts()
        {
            var products = await _productService.GetProductsAsync();
            var newProducts = products.OrderByDescending(p => p.Id).Take(5); 
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(newProducts);

            return Ok(productDtos);
        }

        [HttpGet("promotions")]
        public async Task<IActionResult> GetPromotions()
        {
            var promotions = await _promotionService.GetPromotionsAsync();
            var promotionDtos = _mapper.Map<IEnumerable<PromotionDto>>(promotions);

            return Ok(promotionDtos);
        }
    }
}