using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class AdminController : Controller
	{
		// GET: Admin

		private VMs vMs = new VMs();

		public ActionResult Admin(string action = "error")
		{

			vMs.GetIP();

			if (action == "error")
			{ }
			else if (action == "exit")
			{
				return View("~/Views/Authorization/Authorization.cshtml");
			}
			else if (action == "registration")
			{
				LoadRegistrationPage();
				return View("~/Views/Registration/Registration.cshtml");
			}
			else if (action.StartsWith("add"))
			{
				vMs.AddIp(action);
			}
			else if (action.StartsWith("delete"))
			{
				vMs.DeleteIp(action);
			}
			else
			{
				vMs.Commands(action);
			}

			vMs.GetIP();
			GetVM();
			return View();
		}

		public void GetVM()
		{
			vMs.GetAllVMFromUtill();

			ViewBag.NameVM = vMs.Vm.NameVM;
			ViewBag.StateVM = vMs.Vm.StateVM;
			ViewBag.AllIp = vMs.Vm.IP;
			ViewBag.Ip = vMs.IpDb;
		}

		public void LoadRegistrationPage()
		{
			VMs vMs = new VMs();

			vMs.GetVMs();
			VM vm = vMs.Vm;

			ViewBag.NameVM = vm.NameVM;
			ViewBag.AllIp = vm.IP;
		}
	}
}