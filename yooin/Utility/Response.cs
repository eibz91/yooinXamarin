using System;
namespace yooin
{
	public class Response
	{
		public string sMessage
		{
			get;
			set;
		}
		public object objExtra
		{
			get;
			set;
		}
		public ErrorCode iErrorCode
		{
			get;
			set;
		}
	}

	public enum ErrorCode { 
		NoError = 0,
		SignUp = 1,
		Login = 2,
		DeviceRegistration=3
	}
}
