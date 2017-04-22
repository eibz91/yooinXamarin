using System;
using System.IO;
using yooin.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace yooin.iOS
{
	public class SQLite_iOS : Isqlite
	{
		public SQLite_iOS()
		{
		}

		#region ISQLite implementation
		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "YOOINv1.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);
			// Return the database connection
			return conn;
		}
		#endregion

	}
}
