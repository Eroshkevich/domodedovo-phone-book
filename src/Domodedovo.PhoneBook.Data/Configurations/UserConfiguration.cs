using Domodedovo.PhoneBook.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domodedovo.PhoneBook.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.PhoneNumber).HasMaxLength(15);

            builder.OwnsOne(e => e.Name, navBuilder =>
            {
                navBuilder.Property(e => e.First);
                navBuilder.Property(e => e.Last);
            });
        }
    }
}