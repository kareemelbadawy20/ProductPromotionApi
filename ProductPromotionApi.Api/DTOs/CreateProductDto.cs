using System.ComponentModel.DataAnnotations;

namespace ProductPromotionApi.Api.DTOs
{
        public class CreateProductDto
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
            public decimal Price { get; set; }
        }
}
