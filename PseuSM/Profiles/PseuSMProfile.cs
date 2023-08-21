﻿using AutoMapper;
using PseuSM.Entities;
using AdapterRegisterUser = Adapters.Entities.RegisterUser;
using AdapterLoginUser = Adapters.Entities.LoginUser;
using AdaptersJwtToken = Adapters.Entities.JwtToken;
using AdaptersImage = Adapters.Entities.Image;
using AdaptersUser = Adapters.Entities.User;
using AdaptersSearchResult = Adapters.Entities.SearchResult;
using PseuSM.Models;
using Core.Enums;

namespace PseuSM.Profiles
{
    public class PseuSMProfile : Profile
    {
        public PseuSMProfile()
        {
            CreateMap<AdaptersJwtToken, JwtResponse>().ReverseMap();
            CreateMap<AdapterLoginUser, LoginUser>().ReverseMap();
            CreateMap<AdaptersImage, Image>().ReverseMap();
            CreateMap<AdaptersImage, UserOnlyId>().ReverseMap();
            CreateMap<AdaptersSearchResult, SearchResult>().ReverseMap();

            CreateMap<AdaptersUser, User>()
                .ForMember(
                    user => user.AvatarReference,
                    config => config.MapFrom(aUser => aUser.Images.FirstOrDefault(image => image.Type == ImageTypes.Avatar)!.Reference)
                )
                .ReverseMap();

            CreateMap<AdapterRegisterUser, RegisterUser>()
                .ForMember(
                    user => user.Avatar,
                    config => config.MapFrom(adapterUser => adapterUser.AvatarStream)
                )
                .ReverseMap();

            CreateMap<RegisterUser, RegisterUserModel>()
                .ForMember(
                    userModel => userModel.Avatar,
                    config => config.Ignore()
                )
                .ReverseMap();

            CreateMap<RegisterUserModel, RegisterUser>()
                .ForMember(
                    user => user.Avatar,
                    config => config.MapFrom(userModel => userModel.Avatar != null && userModel.Avatar.Length != 0 ? userModel.Avatar.OpenReadStream() : null)
                )
                .ReverseMap();

            CreateMap<AdaptersUser, UserPreview>()
                .ForMember(
                    up => up.AvatarReference,
                    config => config.MapFrom(au => au.Images.FirstOrDefault(image => image.Type == ImageTypes.Avatar)!.Reference)
                )
                .ReverseMap();

            //Fake data

        }
    }
}
