using AutoMapper;
using BLL.Entities;
using DALUser = DAL.Entities.User;

namespace BLL.Profiles
{
    public class BLLProfile : Profile
    {
        public BLLProfile()
        {
            CreateMap<DALUser, User>().ReverseMap();
        }
    }
}
