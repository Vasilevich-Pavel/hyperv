using System;
using System.Diagnostics;
using System.Text;

namespace WebApplication1.Models
{
	public class UtillProcess
	{
		private string path = @"..\ps\PsExec.exe";

		public VM Vm { get; set; } = new VM();

		public void GetVM(string oneIp)
		{
			GetAllCoding();
			ParseDataUtill(UtillCommand(oneIp), oneIp);
		}

		public void GetVM(string oneIp, string VMName)
		{
			GetAllCoding();
			ParseDataUtill(UtillCommand(oneIp), oneIp, VMName);
		}

		private void GetAllCoding()
		{
			EncodingProvider provider = CodePagesEncodingProvider.Instance;
			Encoding.RegisterProvider(provider);
		}

		private void ParseDataUtill(string allData, string oneIp)
		{
			GetAllVM(ArrayData(allData), oneIp);	
		}

		private void ParseDataUtill(string allData, string oneIp, string VMName)
		{
			GetUserVM(ArrayData(allData), oneIp, VMName);
		}

		private string[] ArrayData(string allData)
		{
			string sub1 = "-";

			int index1 = allData.LastIndexOf(sub1);
			string name = allData.Substring(index1 + 3);

			return name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
		}

		private void GetAllVM(string[] arrayAllData, string oneIp)
		{
			for (int i = 0; i < arrayAllData.Length; i++)
			{
				if (i == 1 || i % 8 == 1)
				{
					arrayAllData[i] = arrayAllData[i].Trim();
					if (arrayAllData[i] == "Off" || arrayAllData[i] == "Paused" || arrayAllData[i] == "Running")
					{
						Vm.StateVM.Add(arrayAllData[i]);
						Vm.NameVM.Add(arrayAllData[i - 1].Trim());
						Vm.IP.Add(oneIp);
					}
				}
			}
		}

		private void GetUserVM(string[] arrayAllData, string oneIp, string VMName)
		{
			for (int i = 0; i < arrayAllData.Length; i++)
			{
				if (i == 1 || i % 8 == 1)
				{
					arrayAllData[i] = arrayAllData[i].Trim();
					if (arrayAllData[i] == "Off" || arrayAllData[i] == "Paused" || arrayAllData[i] == "Running")
					{
						if (arrayAllData[i - 1].Trim() == VMName) {
							Vm.StateVM.Add(arrayAllData[i]);
							Vm.NameVM.Add(arrayAllData[i - 1].Trim());
							Vm.IP.Add(oneIp);
						}
					}
				}
			}
		}

		private string UtillCommand(string oneIp)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.FileName = path;
			p.StartInfo.Arguments = @"\\" + oneIp + " powershell get-vm";
			p.Start();

			string data = p.StandardOutput.ReadToEnd();
			p.Dispose();

			return data;
		}

		private void CommandVM(string name, string ip, string type)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.FileName = path;
			p.StartInfo.Arguments = @"\\" + ip + " powershell " + type + " -name " + name;

			if (type == "stop-vm")
			{
				p.StartInfo.Arguments += " -turnoff";
			}

			p.Start();
			p.Dispose();
		}

		public void Commads(string action)
		{
			string[] arrayAllData = action.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

			switch (arrayAllData[0])
			{
				case "start":

					CommandVM(arrayAllData[1], arrayAllData[2], "start-vm");
					break;

				case "pause":

					CommandVM(arrayAllData[1], arrayAllData[2], "suspend-vm");
					break;

				case "stop":

					CommandVM(arrayAllData[1], arrayAllData[2], "stop-vm");
					break;

				default: break;
			}
		}
	}
}