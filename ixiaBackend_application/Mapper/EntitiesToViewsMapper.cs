using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using Microsoft.AspNetCore.Identity;

namespace ixiaBackend_application.Mapper
{
    public class EntitiesToViewsMapper : AutoMapper.Profile
    {
        public EntitiesToViewsMapper()
        {
            CreateMap<User, UserView>();
            CreateMap<Favorite, FavoriteView>();
            CreateMap<Product, ProductView>();
            CreateMap<ProfileInput, User>();
            CreateMap<Purchase, PurchaseView>();
            CreateMap<Category, CategoryView>();
            CreateMap<Company, CompanyView>();
            CreateMap<IdentityUser, UserView>();
        }
    }
}
