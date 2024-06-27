using Serve.Enums;

namespace Server.Models
{
	public class ProductsModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public float Value { get; set; } = 0;
		public string Description { get; set; }
		public StatusProducts Status { get; set; }
		public int UserID { get; set; }
		public virtual UserModel User { get; set; }
    }
}