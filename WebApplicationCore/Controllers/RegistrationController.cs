using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Registration(string action)
        {

			if (action.StartsWith("checkbox"))
			{
				GetNewUser(action);
			}
			else
			{
				LoadAdminPage();
				return View("~/Views/Admin/Admin.cshtml");
			}
			VMs vMs = new VMs();

			vMs.GetVMs();
			VM vm = vMs.Vm;

			ViewBag.NameVM = vm.NameVM;
			ViewBag.AllIp = vm.IP;

			return View();
        }

		private string GetNewUser(string action)
		{
			string[] data = action.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

			Account account = new Account
			{
				Login = data[1],
				Password = data[2]
			};

			Db db = new Db();
			db.Initialization();
			var id = db.InsertUser(account);

			if (id == -1)
			{
				return "no";
			}

			for (int i = 3; i < data.Length; i += 2)
			{
				VMUser vM = new VMUser();

				vM.NameVM = data[i];
				vM.Ip = data[i + 1];
				vM.Id_account = id;

				db.InsertVM(vM);
			}

			return "ok";
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
    }
}