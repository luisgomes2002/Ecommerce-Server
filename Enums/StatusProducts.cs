using System.ComponentModel;

namespace Serve.Enums
{
	public enum StatusProducts
	{
		[Description("Indisponivel")]
		Unavailable = 0,

		[Description("Disponivel")]
		available = 1,

		[Description("Enviado")]
		Sent = 2,
	}
}