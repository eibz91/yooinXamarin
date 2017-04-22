using System;
using SQLite.Net;

namespace yooin
{
	public interface Isqlite
	{
		SQLiteConnection GetConnection();
	}
}
