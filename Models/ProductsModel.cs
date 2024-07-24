using Serve.Enums;

namespace Server.Models
{
	public class ProductsModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public float Price { get; set; } = 0;
		public string Description { get; set; } = string.Empty;
		public StatusProducts Status { get; set; }
		public int UserId { get; set; }
		public string? UserName { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}