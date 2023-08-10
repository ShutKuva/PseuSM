using Adapters.Entities;
using AutoMapper;
using BLLUser = BLL.Entities.User;

namespace Adapters.Profiles
{
    public class AdapterProfile : Profile
    {
        public AdapterProfile()
        {
            CreateMap<BLLUser, RegisterUser>().ReverseMap();
            CreateMap<BLLUser, LoginUser>().ReverseMap();
        }
    }
}
