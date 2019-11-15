using System.Collections.Generic;

namespace WebApplication1.Models
{
	public class VM
	{
		public List<string> NameVM { get; set; } = new List<string>();
		public List<string> StateVM { get; set; } = new List<string>();
		public List<string> IP { get; set; } = new List<string>();
	}
}