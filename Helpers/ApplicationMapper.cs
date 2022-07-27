using AutoMapper;
using Product_Management_API.Data;
using Product_Management_API.Models;

namespace Product_Management_API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
