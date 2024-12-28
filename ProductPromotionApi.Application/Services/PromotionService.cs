using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductPromotionApi.Application.Interfaces;
using ProductPromotionApi.Core.Entities;

namespace ProductPromotionApi.Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsAsync()
        {
            return await _promotionRepository.GetPromotionsAsync();
        }

        public async Task<Promotion> GetPromotionByIdAsync(int id)
        {
            return await _promotionRepository.GetPromotionByIdAsync(id);
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            await _promotionRepository.AddPromotionAsync(promotion);
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            await _promotionRepository.UpdatePromotionAsync(promotion);
        }

        public async Task DeletePromotionAsync(int id)
        {
            await _promotionRepository.DeletePromotionAsync(id);
        }
    }

}
