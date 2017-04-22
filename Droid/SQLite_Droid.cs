using System;
using Xamarin.Forms;
using yooin.Droid;
using System.IO;

[assembly: Dependency(typeof(SQLite_Droid))]
namespace yooin.Droid
{
	public class SQLite_Droid : Isqlite
	{
		public SQLite_Droid()
		{
		}


		public SQLite.Net.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "YOOINv1.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);
			// Return the database connection 
			return conn;
		}



		/// <summary>
		/// helper method to get the database out of /raw/ and into the user filesystem
		/// </summary>
		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}
