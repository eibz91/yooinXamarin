using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Xamarin.Forms;

namespace yooin.Droid
{
	[Activity(Label = "yooin", Icon = "@drawable/yoiinlogo",Theme = "@style/MyTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity 
	{
		private Action<int, Result, Intent> _resultCallback;
		public static MainActivity CurrentActivity { get; private set; }
		protected override void OnCreate(Bundle bundle)
		{
			CurrentActivity = this;
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			string version = Android.OS.Build.VERSION.Release;
			LoadApplication(new App(version));
			try
			{
				//var asdas = ((Activity)Forms.Context).Window;
				//asdas.SetSoftInputMode(SoftInput.AdjustResize);
				//asdas.SetClipToOutline(false);
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);
				GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);

			}
			catch (Exception ex)
	        {
				System.Diagnostics.Debug.WriteLine(ex.Message); 
	        }
		}



		//file chooser
		public void StartActivity2(Intent intent, int requestCode, Action<int, Result, Intent> resultCallback)
		{
			_resultCallback = resultCallback;
			StartActivityForResult(intent, requestCode);
		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (_resultCallback != null)
			{
				_resultCallback(requestCode, resultCode, data);
				_resultCallback = null;
			}
		}
	}
}
