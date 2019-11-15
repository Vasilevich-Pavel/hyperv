using Microsoft.Extensions.Configuration;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class Db
	{
		private static string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
		private static IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath).AddJsonFile("appsettings.json").Build();
		private static string connectionString = configuration.GetConnectionString("DefaultConnection");
		readonly OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, PostgreSqlDialect.Provider);

		public void Initialization()
		{
			using (var db = dbFactory.Open())
			{ 
				db.CreateTable<IP>();
				db.CreateTable<Account>();
				db.CreateTable<VMUser>();
			}
		}

		public void InsertIp(IP ip)
		{
			using (var db = dbFactory.Open())
			{
				try
				{
					db.Insert<IP>(ip);
				}
				catch { }
			}
		}

		public List<IP> SelectIp()
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<IP>();
			}
		}

		public void DeleteIp(IP ip)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<IP>(x => x.Ip == ip.Ip);
			}
		}

		public long InsertUser(Account account)
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<Account>(x => x.Login == account.Login);

				if (single == null)
				{

					account.Position = "user";
					long id = db.Insert(account, selectIdentity: true);
					return id;
				}

				return -1;
			}
		}

		public void InsertAdmin()
		{
			using (var db = dbFactory.Open())
			{
				try
				{
					Account account = new Account
					{
						Login = "admin",
						Password = "admin",
						Position = "admin"
					};

					var single = db.Single<Account>(x => x.Login == "admin" && x.Password == "admin" && x.Position == "admin");

					if (single == null)
					{
						db.Insert<Account>(account);
					}
				}
				catch { }
			}
		}

		public List<Account> SelectUser()
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<Account>(x => x.Position == "user");
			}
		}

		public void DeleteUser(Account account)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<Account>(x => x.Login == account.Login);
			}
		}

		public List<Account> SelectAllAccount()
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<Account>();
			}
		}

		public void InsertVM(VMUser vM)
		{
			using (var db = dbFactory.Open())
			{
				db.Insert<VMUser>(vM);
			}
		}

		public List<VMUser> SelectVM(int id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<VMUser>(x => x.Id_account == id);
			}
		}

	}
}