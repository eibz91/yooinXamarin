using System;
using System.Linq;
using SQLite;
using SQLite.Net;
using Xamarin.Forms;

namespace yooin
{
	public class DataBaseAction
	{

		static object locker = new object();

		SQLiteConnection database;


		public DataBaseAction()
		{

			try
			{
				database = DependencyService.Get<Isqlite>().GetConnection();
				database.CreateTable<tLinks>();
				database.CreateTable<tPush>();
				database.CreateTable<tUser>();


			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + " DataBaseAction");
			}

		}

	
		public bool deleteAllTlink()
		{

			lock (locker)
			{
				try
				{
					database.DeleteAll<tLinks>();
					return true;
				}
				catch (Exception ex)
				{

					System.Diagnostics.Debug.WriteLine(ex.Message + "deletettLinks");
					return false;
				}

			}
		}
		public bool insertLink(tLinks obj)
		{
			lock (locker)
			{
				try
				{
					database.DeleteAll<tLinks>();
					database.Insert(obj);
					return true;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "inserttLinks");
					return false;
				}

			}
		}
		public tLinks isLinkInDataBase()
		{
			lock (locker)
			{
				try
				{
					var data = (from i in database.Table<tLinks>() select i).ToList();
					return ((from i in database.Table<tLinks>() select i).Count() >= 1 ? data[0] : null);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "isElementInDataBase");
					return null;
				}
			}
		}

		public tPush isPushInDataBase()
		{
			lock (locker)
			{
				try
				{
					var data = (from i in database.Table<tPush>() select i).ToList();
					return ((from i in database.Table<tPush>() select i).Count() >= 1 ? data[0] : null);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "isElementInDataBasetPush");
					return null;
				}
			}
		}

		public bool insertPush(tPush obj)
		{
			lock (locker)
			{
				try
				{
					database.DeleteAll<tPush>();
					database.Insert(obj);
					return true;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "inserttPush");
					return false;
				}

			}
		}

		public bool insertUser(tUser obj)
		{
			lock (locker)
			{
				try
				{
					database.DeleteAll<tUser>();
					database.Insert(obj);
					return true;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "inserttUser");
					return false;
				}

			}
		}
		public tUser isUserInDataBase()
		{
			lock (locker)
			{
				try
				{
					var data = (from i in database.Table<tUser>() select i).ToList();
					return ((from i in database.Table<tUser>() select i).Count() >= 1 ? data[0] : null);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "isElementInDataBase");
					return null;
				}
			}
		}
	}
}
