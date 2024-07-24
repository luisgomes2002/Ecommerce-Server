namespace Server.Models
{
	public class UsersModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string? Phone { get; set; } = null;
		public string? Address { get; set; } = null;
		public string? City { get; set; } = null;
		public string? Region { get; set; }	= null;
		public string? PostalCode { get; set; } = null;
		public string? Country { get; set; } = null;
		public string[]? Cart { get; set; }
		public bool IsMod { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}