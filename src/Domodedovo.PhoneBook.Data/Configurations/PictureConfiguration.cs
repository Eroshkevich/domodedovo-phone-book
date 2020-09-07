using Domodedovo.PhoneBook.Data.Models;
using Domodedovo.PhoneBook.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domodedovo.PhoneBook.Data.Configurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            var enumToStringConverter = new EnumToStringConverter<PictureType>();
            builder.Property(e => e.Type).HasConversion(enumToStringConverter).IsRequired();
            
            var uriToStringConverter = new UriToStringConverter();
            builder.Property(e => e.Url).HasConversion(uriToStringConverter);
        }
    }
}