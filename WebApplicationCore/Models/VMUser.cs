using ServiceStack.DataAnnotations;

namespace WebApplication1.Models
{
	[Alias("VMUser")]
	public class VMUser
	{
		[AutoIncrement]
		public int Id { get; set; }

		[StringLength(100)]
		public string NameVM {get; set; }

		[StringLength(25)]
		public string Ip { get; set; }

		[ForeignKey(typeof(Account))]
		public long Id_account { get; set; }
	}
}