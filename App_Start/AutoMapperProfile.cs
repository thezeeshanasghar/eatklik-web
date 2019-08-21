
using AutoMapper;
using eatklik.DTOs;
using eatklik.Models;

namespace eatklik
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>().ReverseMap();
            CreateMap<Cuisine, CuisineDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }

    }

}