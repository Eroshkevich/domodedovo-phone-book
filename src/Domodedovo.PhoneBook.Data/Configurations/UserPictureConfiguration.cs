using Domodedovo.PhoneBook.Data.Models.Entities;
using Domodedovo.PhoneBook.Data.Models.Links;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domodedovo.PhoneBook.Data.Configurations
{
    public class UserPictureConfiguration : IEntityTypeConfiguration<UserPicture>
    {
        public void Configure(EntityTypeBuilder<UserPicture> builder)
        {
            builder.HasKey(e => new {e.UserId, e.PictureId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.Pictures)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Picture)
                .WithOne()
                .HasForeignKey<UserPicture>(e => e.PictureId)
                .HasPrincipalKey<Picture>(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}