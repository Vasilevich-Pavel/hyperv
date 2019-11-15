using ServiceStack.DataAnnotations;

namespace WebApplication1.Models
{
	[Alias("Account")]
	public class Account
	{
		[AutoIncrement]
		public int Id { get; set; }

		[Unique]
		[StringLength(25)]
		public string Login { get; set; }

		[StringLength(25)]
		public string Password { get; set; }

		[StringLength(10)]
		public string  Position { get; set; }
	}
}