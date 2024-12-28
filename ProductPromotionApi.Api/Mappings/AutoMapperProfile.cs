using AutoMapper;
using ProductPromotionApi.Api.DTOs;
using ProductPromotionApi.Core.Entities;

namespace ProductPromotionApi.Api.Mappings
{
   
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<CreateProductDto, Product>();
                CreateMap<UpdateProductDto, Product>();
                CreateMap<Product, ProductDto>();

                CreateMap<CreatePromotionDto, Promotion>();
                CreateMap<UpdatePromotionDto, Promotion>();
                CreateMap<Promotion, PromotionDto>();
            }
        }
    

}
