using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Models;

namespace Serve.Data.Map
{
	public class ProductsMap : IEntityTypeConfiguration<ProductsModel>
	{
		public void Configure(EntityTypeBuilder<ProductsModel> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
			builder.Property(x => x.Value).IsRequired();
			builder.Property(x => x.Description).HasMaxLength(1000);
			builder.Property(x => x.Status).IsRequired();
			builder.Property(x => x.UserID).IsRequired();

			builder.HasOne(x => x.User);
		}
	}
}