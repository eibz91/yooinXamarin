using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace yooin
{
	public class Connection
	{

		public double iStatusResponse { get; set; }
		public double TimeOut { get; set; }
		public string sParameter { get; set; }
		public string sUrl { get; set; }

		HttpResponseMessage httpResponse;
		HttpClientHandler objCredential;
		HttpClient httpRequest;
		string sJson;

		public Connection()
		{
			httpResponse = null;
			objCredential = new HttpClientHandler();
			TimeOut = 45000;
		}
		public void setCredentials(string sUser, string sPass, string sDomain)
		{
			objCredential.Credentials = new NetworkCredential(sUser, sPass, sDomain);
		}
		public void setCredentials(string sUser, string sPass)
		{
			objCredential.Credentials = new NetworkCredential(sUser, sPass);
		}
		public async Task<string> getMethod()
		{

			try
			{
				httpRequest = new HttpClient();
				httpRequest.Timeout = TimeSpan.FromMilliseconds(TimeOut);
				httpResponse = await httpRequest.GetAsync(sUrl);
				iStatusResponse = (double)httpResponse.StatusCode;
				sJson = httpResponse.Content.ReadAsStringAsync().Result;
				return sJson;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return "" ;
			}
		}
		public async Task<string> getMethodSecure()
		{

			try
			{
				httpRequest = new HttpClient(objCredential);
				httpRequest.Timeout = TimeSpan.FromMilliseconds(TimeOut);
				httpResponse = await httpRequest.GetAsync(sUrl);
				iStatusResponse = (double)httpResponse.StatusCode;
				sJson = httpResponse.Content.ReadAsStringAsync().Result;
				return sJson;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return "";
			}
		}
		public async Task<string> postMethod()
		{
			try
			{
				StringContent httpContent = new StringContent(sParameter, System.Text.Encoding.UTF8, "application/json");
				httpRequest = new HttpClient();
				httpRequest.Timeout = TimeSpan.FromMilliseconds(TimeOut);
				httpRequest.BaseAddress = new Uri(sUrl);
				httpResponse = await httpRequest.PostAsync(sUrl, httpContent);
				iStatusResponse = (double)httpResponse.StatusCode;
				sJson = httpResponse.Content.ReadAsStringAsync().Result;
				return sJson;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return "";
			}
		}
		public async Task<string> postMethodSecure()
		{
			try
			{
				StringContent httpContent = new StringContent(sParameter, System.Text.Encoding.UTF8, "application/json");
				httpRequest = new HttpClient(objCredential);
				httpRequest.Timeout = TimeSpan.FromMilliseconds(TimeOut);
				httpRequest.BaseAddress = new Uri(sUrl);
				httpResponse = await httpRequest.PostAsync(sUrl, httpContent);
				iStatusResponse = (double)httpResponse.StatusCode;
				sJson = httpResponse.Content.ReadAsStringAsync().Result;
				return sJson;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return "";
			}
		}
	}
}
