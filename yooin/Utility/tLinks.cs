using System;

namespace yooin
{
	public class tLinks
	{
		public string baseWebUrl
		{
			get;
			set;
		}public string baseWebApi
		{
			get;
			set;
		}

		public static string getWebUr()
		{
			tLinks objLink = App.Database.isLinkInDataBase();
			return (objLink != null) ? objLink.baseWebUrl : "";
		}
		public static string getApiUrl()
		{
			tLinks objLink = App.Database.isLinkInDataBase();
			return (objLink != null) ? objLink.baseWebApi : "";
		}
	}
}
