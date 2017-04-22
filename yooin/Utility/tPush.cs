using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace yooin
{
	public class tPush
	{
		public string deviceToken
		{
			get;
			set;
		}
		public int iTipo
		{
			get;
			set;
		}
		public int iCliente {
			get;
			set;
		
		}
		public int iIdevice
		{
			get;
			set;
		}
		private static int identity;

		public static void saveLocal(string token,int iTipo) {
			tPush objLink = App.Database.isPushInDataBase();
			if (objLink == null)
			{
				App.Database.insertPush(
					new tPush
					{
						deviceToken = token,
						iTipo = iTipo,
						iCliente = 0,
						iIdevice = 0
					}
				);
			}
			else
			{
				if (objLink.deviceToken != token)
				{
					App.Database.insertPush(
						new tPush
						{
							deviceToken = token,
							iTipo = iTipo,
							iCliente = 0,
							iIdevice = 0
						}
					);
				}
			}
		}
		public static async void saveCloud(int iCliente)
		{
			string deviceToken = String.Empty;
			int iTipo = 0;
			int iClienteSaved = 0;
			int iDevice = 0;
			tPush objLink = App.Database.isPushInDataBase();
			if (objLink != null)
			{
				deviceToken = objLink.deviceToken;
				iTipo = objLink.iTipo;
				iClienteSaved = objLink.iCliente;
				iDevice = objLink.iIdevice;
				if (await wasSaved(iCliente,deviceToken,iTipo,iDevice))
				{
					App.Database.insertPush(
						new tPush
						{
							deviceToken = deviceToken,
							iTipo = iTipo,
							iCliente = iCliente,
							iIdevice = identity
						}
					);
				}
			}
		}

		private static async System.Threading.Tasks.Task<bool> wasSaved(int iIdCLiente, string sPushId, int iTipo,int iDevice)
		{
			Connection objConnection = new Connection();
			objConnection.sUrl = tLinks.getApiUrl() + EndPoints.AddPushDevice;
			var obj = new
			{
				iIdCliente = iIdCLiente,
				sPushId = sPushId,
				iTipo=iTipo,
				iIdDispositivo = iDevice
			};
			objConnection.sParameter = JsonConvert.SerializeObject(obj);
			var response = await objConnection.postMethod();
			if (response != "")
			{
				try
				{
					Response objResponse = JsonConvert.DeserializeObject<Response>(response);
					if (objResponse.iErrorCode == ErrorCode.NoError)
					{

						JObject algo = (JObject)objResponse.objExtra;
						identity = (int)algo["iIdDevice"];
						return true;
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					return false;
				}
			}
			return false;
		}

		public static async void deleteUserPush()
		{
			string deviceToken = String.Empty;
			int iTipo = 0;
			int iClienteSaved = 0;
			int iDevice = 0;
			tPush objLink = App.Database.isPushInDataBase();
			if (objLink != null)
			{
				deviceToken = objLink.deviceToken;
				iTipo = objLink.iTipo;
				iClienteSaved = objLink.iCliente;
				iDevice = objLink.iIdevice;
				if (iClienteSaved != 0)
				{

					await deleteUser(iDevice);

					App.Database.insertPush(
						new tPush
						{
							deviceToken = deviceToken,
							iTipo = iTipo,
							iCliente = 0
						}
					);
				}
			}
		}

		private static async System.Threading.Tasks.Task<bool> deleteUser(int iDevice)
		{
			Connection objConnection = new Connection();
			objConnection.sUrl = tLinks.getApiUrl() + EndPoints.DeletePushDevice;
			var obj = new
			{
				iIdDispositivo = iDevice
			};
			objConnection.sParameter = JsonConvert.SerializeObject(obj);
			var response = await objConnection.postMethod();
			if (response != "")
			{
				try
				{
					Response objResponse = JsonConvert.DeserializeObject<Response>(response);
					if (objResponse.iErrorCode == ErrorCode.NoError)
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					return false;
				}
			}
			return false;
		}

	}
}
