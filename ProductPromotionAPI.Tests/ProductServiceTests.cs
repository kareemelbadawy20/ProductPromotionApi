using Moq;
using ProductPromotionApi.Application.Interfaces;
using ProductPromotionApi.Application.Services;
using ProductPromotionApi.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductPromotionAPI.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<IPromotionRepository> _mockPromotionRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockPromotionRepo = new Mock<IPromotionRepository>();
            _service = new ProductService(_mockProductRepo.Object, _mockPromotionRepo.Object);
        }

        [Fact]
        public async Task ApplyPromotionAsync_ValidProductAndPromotion_UpdatesProduct()
        {
            // Arrange
            var productId = 1;
            var promotionId = 1;
            var product = new Product { Id = productId, Name = "Product1", ProductPromotions = new List<ProductPromotion>() };
            var promotion = new Promotion { Id = promotionId, Name = "Promo1" };

            _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(product);
            _mockPromotionRepo.Setup(repo => repo.GetPromotionByIdAsync(promotionId)).ReturnsAsync(promotion);

            // Act
            await _service.ApplyPromotionAsync(productId, promotionId);

            // Assert
            _mockProductRepo.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.ProductPromotions.Count == 1 && p.ProductPromotions.ElementAt(0).PromotionId == promotionId)), Times.Once);
        }

        [Fact]
        public async Task ApplyPromotionAsync_InvalidProduct_ThrowsException()
        {
            // Arrange
            var productId = 1;
            var promotionId = 1;

            _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ApplyPromotionAsync(productId, promotionId));
        }

        [Fact]
        public async Task ApplyPromotionAsync_InvalidPromotion_ThrowsException()
        {
            // Arrange
            var productId = 1;
            var promotionId = 1;
            var product = new Product { Id = productId, Name = "Product1", ProductPromotions = new List<ProductPromotion>() };

            _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(product);
            _mockPromotionRepo.Setup(repo => repo.GetPromotionByIdAsync(promotionId)).ReturnsAsync((Promotion)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ApplyPromotionAsync(productId, promotionId));
        }

        [Fact]
        public async Task ApplyPromotionAsync_PromotionAlreadyApplied_ThrowsException()
        {
            // Arrange
            var productId = 1;
            var promotionId = 1;
            var product = new Product
            {
                Id = productId,
                Name = "Product1",
                ProductPromotions = new List<ProductPromotion>
                {
                    new ProductPromotion { ProductId = productId, PromotionId = promotionId }
                }
            };

            _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(product);
            _mockPromotionRepo.Setup(repo => repo.GetPromotionByIdAsync(promotionId)).ReturnsAsync(new Promotion { Id = promotionId, Name = "Promo1" });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ApplyPromotionAsync(productId, promotionId));
        }
    }
}
