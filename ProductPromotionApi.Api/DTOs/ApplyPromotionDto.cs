using System.ComponentModel.DataAnnotations;

namespace ProductPromotionApi.Api.DTOs
{
 
        public class ApplyPromotionDto
        {
            [Required]
            public int ProductId { get; set; }

            [Required]
            public int PromotionId { get; set; }
        }
    
}
