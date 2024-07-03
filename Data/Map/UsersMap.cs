using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Serve.Data.Map
{
	public class UsersMap : IEntityTypeConfiguration<UsersModel>
	{
		public void Configure(EntityTypeBuilder<UsersModel> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
			builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Phone).HasMaxLength(11);
            builder.Property(x => x.Address).HasMaxLength(100);
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.Region).HasMaxLength(50);
            builder.Property(x => x.PostalCode).HasMaxLength(8);
            builder.Property(x => x.Country).HasMaxLength(50);
            builder.Property(x => x.Cart).HasMaxLength(1000);
			builder.Property(x => x.IsMod).IsRequired().HasMaxLength(5);
        }
	}
}