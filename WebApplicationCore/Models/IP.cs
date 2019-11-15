using ServiceStack.DataAnnotations;

namespace WebApplication1.Controllers
{
	[Alias("IP")]
	public class IP
	{
		[Unique]
		[AutoIncrement]
		public int Id { get; set; }

		[Unique]
		[StringLength(25)]
		public string Ip { get; set; }

	}
}