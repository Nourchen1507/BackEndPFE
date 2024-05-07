using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using AutoMapper;

namespace App.UI.Web
{
    public class AutoMapperProfile : Profile
    {  
        public AutoMapperProfile() {




            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
    
            CreateMap<User, ReadUserDto>();
            CreateMap<ReadUserDto, User>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<Product, ReadProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<ReadProductDto, Product>();

            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<Category, ReadCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<ReadCategoryDto, Category>();

            CreateMap<Order, CreateOrderDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<Order, ReadOrderDto>();
            CreateMap<ReadOrderDto, Order>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<ReadOrderDto, OrderItem>();
            CreateMap<OrderItem, ReadOrderItemDto>();

            CreateMap<OrderItemDto, Order>();
            CreateMap<Order, OrderItemDto>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<SoldeCarte, SoldeCarteDto>();
            CreateMap<UserSoldeDto, UserSoldeDto>();

            CreateMap<CreateAdresseDto, Adresse>();
            CreateMap<Adresse, CreateAdresseDto>();
        
            CreateMap<Adresse, UpdateAdresseDto>();
            CreateMap<Adresse, ReadAdresseDto>();
            CreateMap<CreateAdresseDto, Adresse>();
            CreateMap<UpdateAdresseDto, Adresse>();
            CreateMap<ReadAdresseDto, Adresse>();


        }

    }
}
