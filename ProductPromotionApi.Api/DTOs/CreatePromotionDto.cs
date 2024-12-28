using System.ComponentModel.DataAnnotations;

namespace ProductPromotionApi.Api.DTOs
{
   
        public class CreatePromotionDto
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [Range(0.01, 100.00, ErrorMessage = "Discount must be between 0.01 and 100.00")]
            public decimal Discount { get; set; }
        }
    

}
