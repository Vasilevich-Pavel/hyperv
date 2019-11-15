using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class UserController : Controller
	{
		// GET: User

		private VMs vMs = new VMs();
		private List<VMUser> vMUsers = new List<VMUser>();
		public static int Id { get; set; }

		public new ActionResult User(string action = "exit")
        {

			if (action == "exit")
			{
				return View("~/Views/Authorization/Authorization.cshtml");
			}
			else
			{
				vMs.Commands(action);
			}

			GetUpdateVM();

			return View();
        }

		public void GetUpdateVM()
		{
			Db db = new Db();
			vMUsers = db.SelectVM(Id);

			VMs vMs = new VMs();
			vMs.GetUserVMs(vMUsers);

			ViewBag.NameVM = vMs.Vm.NameVM;
			ViewBag.StateVM = vMs.Vm.StateVM;
			ViewBag.Ip = vMs.Vm.IP;
		}

	}
}