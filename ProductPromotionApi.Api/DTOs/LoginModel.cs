namespace ProductPromotionApi.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace ProductPromotionAPI.WebAPI.Models
    {
        public class LoginModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }

}
