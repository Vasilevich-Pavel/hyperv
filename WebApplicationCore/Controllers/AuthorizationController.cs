using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class AuthorizationController : Controller
    {
		// GET: Authorization

        public ActionResult Authorization()
		{
			Db db = new Db();
			db.Initialization();
			db.InsertAdmin();

			return View();
        }

		public ViewResult GetLoginAndPassword(string action)
		{
			Db db = new Db();
			db.Initialization();

			var user = db.SelectAllAccount();

			return CheckAuthorization(action, user);
		
		}

		private void LoadAdminPage()
		{
			VMs vMs = new VMs();

			vMs.GetVMs();
			VM vm = vMs.Vm;

			ViewBag.NameVM = vm.NameVM;
			ViewBag.StateVM = vm.StateVM;
			ViewBag.AllIp = vm.IP;
			ViewBag.Ip = vMs.IpDb;
		}

		private void LoadUserPage(int id)
		{
			Db db = new Db();
			var vMUser = db.SelectVM(id);

			VMs vMs = new VMs();
			vMs.GetUserVMs(vMUser);

			ViewBag.NameVM = vMs.Vm.NameVM;
			ViewBag.StateVM = vMs.Vm.StateVM;
			ViewBag.Ip = vMs.Vm.IP;

			UserController.Id = id;

		}

		private ViewResult CheckAuthorization(string action, List<Account> user)
		{
			var data = action.Split('|');

			foreach (var u in user)
			{
				var q = data.Length;

				if (u.Login == data[0] && u.Password == data[1])
				{
					
					if (u.Position == "admin")
					{
						LoadAdminPage();

						return View("~/Views/Admin/Admin.cshtml");
					}

					else if (u.Position == "user")
					{
						LoadUserPage(u.Id);

						return View("~/Views/User/User.cshtml");
					}
				}
			}

			return View("~/Views/Authorization/Authorization.cshtml");
		}
	}
}