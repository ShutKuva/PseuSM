using AutoMapper;
using BLL.Entities;
using DALUser = DAL.Entities.User;
using DALImage = DAL.Entities.Image;
using DALImagePlaceholder = DAL.Entities.ImagePlaceholder;
using DALCloudinaryEntity = DAL.Entities.Cloudinary.CloudinaryEntity;
using DALPost = DAL.Entities.Post;

namespace BLL.Profiles
{
    public class BLLProfile : Profile
    {
        public BLLProfile()
        {
            CreateMap<DALUser, User>().ReverseMap();
            CreateMap<DALImage, Image>().ReverseMap();
            CreateMap<DALImagePlaceholder, ImagePlaceholder>().ReverseMap();
            CreateMap<DALCloudinaryEntity, CloudinaryEntity>().ReverseMap();
            CreateMap<DALPost, Post>().ReverseMap();
        }
    }
}
