using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domodedovo.PhoneBook.Data.DTO;
using Domodedovo.PhoneBook.Data.Models;
using Domodedovo.PhoneBook.Data.Models.Entities;
using Domodedovo.PhoneBook.Data.Models.Links;
using Domodedovo.PhoneBook.Data.Models.ValueTypes;

namespace Domodedovo.PhoneBook.Data.Mappings
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<string, PictureType>().ConvertUsing<StringToEnumConverter<PictureType>>();
            CreateMap<string, Uri>().ConvertUsing<StringToUriConverter>();

            CreateMap<NameDTO, Name>().ReverseMap();

            CreateMap<PictureDTO, Picture>().ReverseMap();

            CreateMap<UserDTO, User>()
                .ForMember(d => d.Pictures, e => e.MapFrom<UserPicturesPicturesValueResolver>())
                .ReverseMap()
                .ForMember(d => d.Pictures, e => e.MapFrom<PicturesUserPicturesValueResolver>());
        }

        private class UserPicturesPicturesValueResolver : IValueResolver<UserDTO, User, ICollection<UserPicture>>
        {
            public ICollection<UserPicture> Resolve(UserDTO source, User destination,
                ICollection<UserPicture> destMember, ResolutionContext context)
            {
                var pictures = context.Mapper.Map<ICollection<Picture>>(source.Pictures);

                return pictures.Select(e => new UserPicture
                {
                    User = destination,
                    Picture = e
                }).ToList();
            }
        }

        private class PicturesUserPicturesValueResolver : IValueResolver<User, UserDTO, ICollection<PictureDTO>>
        {
            public ICollection<PictureDTO> Resolve(User source, UserDTO destination, ICollection<PictureDTO> destMember,
                ResolutionContext context) =>
                context.Mapper.Map<ICollection<PictureDTO>>(source.Pictures.Select(e => e.Picture));
        }
    }
}