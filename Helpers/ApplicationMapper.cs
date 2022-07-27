using AutoMapper;
using Product_Management_API.Data;
using Product_Management_API.Models;

namespace Product_Management_API.Helpers
{
    //Mapping the product with the product model using automapper
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        { CreateMap<Product, ProductModel>().ReverseMap(); }
    }
}
