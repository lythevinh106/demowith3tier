using AutoMapper;
using DTOShared.FetchData;
using DTOShared.Modules.Category.Request;
using DTOShared.Modules.Category.Response;
using DTOShared.Modules.Products.Request;
using DTOShared.Modules.Products.Response;
using DTOShared.Modules.Room.Response;
using DTOShared.Modules.Room.RoomRequest;
using DTOShared.Modules.RoomMember.Request;

using MydemoFirst.Models.Modules.Category.Models;
using MydemoFirst.Models.Modules.Products.Models;
using MydemoFirst.Models.Modules.Room.Models;
using MydemoFirst.Models.Modules.RoomMember.Models;
using MydemoFirst.Models.Modules.User.Models;

namespace MydemoFirst.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //product module
            CreateMap<ProductRequest, Product>().ReverseMap(); //Map from ProductRequest to  Product
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductRequest, ProductResponse>().ReverseMap();
            CreateMap<FetchDataRequest, FetchDataProductRequest>().ReverseMap();

            //Categorymodule
            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<FetchDataRequest, FetchDataCategoryRequest>().ReverseMap();

            //User module

            CreateMap<User, UserInfo>().ReverseMap();


            //Room

            CreateMap<Room, RoomRequest>().ReverseMap();
            CreateMap<Room, RoomResponse>().ReverseMap();
            CreateMap<RoomMember, RoomMemberRequest>().ReverseMap();

        }
    }
}