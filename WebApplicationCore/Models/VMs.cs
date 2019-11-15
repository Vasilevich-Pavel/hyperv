using System;
using System.Collections.Generic;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
	public class VMs
	{

		public VM Vm { get; set; } = new VM();

		public List<string> IpDb { get; set; } = new List<string>();

		private UtillProcess process = new UtillProcess();

		public void Commands(string action)
		{

			GetAllVMFromUtill();

			process.Commads(action);

			ClearVM();
		}

		public void DeleteIp(string action)
		{
			string[] array = action.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
			IP iP = new IP
			{
				Ip = array[1]
			};

			Db db = new Db();
			db.Initialization();
			db.DeleteIp(iP);

			ClearVM();
			GetIP();
		}

		public void AddIp(string action)
		{
			var getIp = action.Substring(3);
			IP newIp = new IP();
			newIp.Ip = getIp;

			Db db = new Db();
			db.Initialization();
			db.InsertIp(newIp);

			ClearVM();
			GetIP();
		}

		public void GetIP()
		{
			IpDb.Clear();

			Db db = new Db();
			db.Initialization();

			var allIp = db.SelectIp();

			foreach (var i in allIp)
			{
				IpDb.Add(i.Ip);
			}

			IpDb.Sort();
		}

		public void GetAllVMFromUtill()
		{
			foreach (var oneIp in IpDb)
			{
				process.GetVM(oneIp);
			}

			Vm = process.Vm;
		}

		public void GetAllVMFromUtill(List<VMUser> vMUser)
		{
			foreach (var vm in vMUser)
			{
				process.GetVM(vm.Ip, vm.NameVM);
			}

			Vm = process.Vm;
		}

		public void GetVMs()
		{
			GetIP();
			GetAllVMFromUtill();
		}

		public void GetUserVMs(List<VMUser> vMUser)
		{
			GetAllVMFromUtill(vMUser);
		}

		private void ClearVM()
		{
			Vm.NameVM.Clear();
			Vm.StateVM.Clear();
			Vm.IP.Clear();
		}
	}
}