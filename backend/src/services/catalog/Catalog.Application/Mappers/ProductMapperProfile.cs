using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
    }
}
