using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductPromotionApi.Application.Interfaces;
using ProductPromotionApi.Core.Entities;

namespace ProductPromotionApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;
        public ProductService(IProductRepository productRepository, IPromotionRepository promotionRepository)
        {
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
        public async Task ApplyPromotionAsync(int productId, int promotionId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            var promotion = await _promotionRepository.GetPromotionByIdAsync(promotionId);
            if (promotion == null) throw new Exception("Promotion not found");

            // Check if the promotion is already applied
            if (product.ProductPromotions == null)
            {
                product.ProductPromotions = new List<ProductPromotion>();
            }

            if (product.ProductPromotions.Any(pp => pp.PromotionId == promotionId))
            {
                throw new Exception("Promotion already applied to this product");
            }

            var productPromotion = new ProductPromotion
            {
                ProductId = productId,
                PromotionId = promotionId,
                Product = product,
                Promotion = promotion
            };

            product.ProductPromotions.Add(productPromotion);
            await _productRepository.UpdateProductAsync(product);
        }


    }

}
