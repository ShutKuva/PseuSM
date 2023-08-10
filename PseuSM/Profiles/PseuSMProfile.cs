using AutoMapper;
using PseuSM.Entities;
using AdapterRegisterUser = Adapters.Entities.RegisterUser;
using AdapterLoginUser = Adapters.Entities.LoginUser;
using AdaptersJwtToken = Adapters.Entities.JwtToken;

namespace PseuSM.Profiles
{
    public class PseuSMProfile : Profile
    {
        public PseuSMProfile()
        {
            CreateMap<AdaptersJwtToken, JwtResponse>().ReverseMap();
            CreateMap<AdapterLoginUser, LoginUser>().ReverseMap();
            CreateMap<AdapterRegisterUser, RegisterUser>().ReverseMap();
        }
    }
}
