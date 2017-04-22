using System;
namespace yooin
{
	public static class EndPoints
	{
		public static readonly string SigUp = "client/signup";
		public static readonly string Login = "client/login";
		public static readonly string AddPushDevice = "device/RecordDevice";
		public static readonly string DeletePushDevice = "device/DeleteDevice";


		public static readonly string FCMSENDERID = "1016385348330";
		public static readonly string DROIDMESSAGE = "gcm.notification.body";
		public static readonly string URLAPIBASE = "http://api.yooin.mx.pine.arvixe.com/api/";
		public static readonly string URLWEBBASE = "http://yooin.mx.pine.arvixe.com/";
		public static readonly string LOGINWEB = "Default.aspx?a=loginFromMovil&b={0}&c={1}";
	}
}
