using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using Domodedovo.PhoneBook.Data.Models;
using Domodedovo.PhoneBook.Integrations.RandomUser.DTO;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Mappings
{
    public class RandomUserProfile : Profile
    {
        private readonly Regex _notANumberRegex = new Regex("[^0-9]");

        public RandomUserProfile()
        {
            var picturesValueConverter = new PicturesValueConverter();
            CreateMap<UserDTO, Data.DTO.UserDTO>()
                .ForMember(d => d.PhoneNumber, e => e.AddTransform(number => _notANumberRegex.Replace(number, "")))
                .ForMember(d => d.Pictures, e => e.ConvertUsing(picturesValueConverter, s => s.Picture));
            
            CreateMap<NameDTO, Data.DTO.NameDTO>();
        }

        private class PicturesValueConverter : IValueConverter<PictureDTO, ICollection<Data.DTO.PictureDTO>>
        {
            public ICollection<Data.DTO.PictureDTO> Convert(PictureDTO sourceMember, ResolutionContext context)
            {
                var pictures = new List<Data.DTO.PictureDTO>();
                if (!string.IsNullOrWhiteSpace(sourceMember.Large))
                    pictures.Add(new Data.DTO.PictureDTO
                    {
                        Type = PictureType.Large.ToString(),
                        Url = sourceMember.Large
                    });
                if (!string.IsNullOrWhiteSpace(sourceMember.Large))
                    pictures.Add(new Data.DTO.PictureDTO
                    {
                        Type = PictureType.Medium.ToString(),
                        Url = sourceMember.Medium
                    });
                if (!string.IsNullOrWhiteSpace(sourceMember.Large))
                    pictures.Add(new Data.DTO.PictureDTO
                    {
                        Type = PictureType.Thumbnail.ToString(),
                        Url = sourceMember.Thumbnail
                    });
                return pictures;
            }
        }
    }
}