using AutoMapper;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Entites.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>()
                .ForMember(
                    dest => dest.BasketItems,
                    opt => opt.MapFrom(src => src.Items));

            CreateMap<BasketDto, CustomerBasket>()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => src.BasketItems));

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
