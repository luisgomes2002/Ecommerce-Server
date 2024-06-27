using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Serve.Data.Map
{
	public class UserMap : IEntityTypeConfiguration<UserModel>
	{
		public void Configure(EntityTypeBuilder<UserModel> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
			builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
		}
	}
}