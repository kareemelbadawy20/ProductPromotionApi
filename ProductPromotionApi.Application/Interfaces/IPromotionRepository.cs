using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductPromotionApi.Core.Entities;

namespace ProductPromotionApi.Application.Interfaces
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetPromotionsAsync();
        Task<Promotion> GetPromotionByIdAsync(int id);
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int id);
    }

}
