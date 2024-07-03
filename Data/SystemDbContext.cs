using Microsoft.EntityFrameworkCore;
using Serve.Data.Map;
using Server.Models;

namespace Server.Data
{
	public class SystemDbContext : DbContext
	{
		public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
		{

		}

		public DbSet<UsersModel> Users { get; set; }
		public DbSet<ProductsModel> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UsersMap());
			modelBuilder.ApplyConfiguration(new ProductsMap());
			base.OnModelCreating(modelBuilder);
		}
	}
}