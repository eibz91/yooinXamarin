using System;
using Android.Webkit;
using Java.Interop;
using Xamarin.Forms;

namespace yooin.Droid
{
	public class JSBridge : Java.Lang.Object
	{
		readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

		public JSBridge(HybridWebViewRenderer hybridRenderer)
		{
			hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
		}

		[JavascriptInterface]
		[Export("invokeAction")]
		public async void InvokeAction(string data)
		{

			Device.BeginInvokeOnMainThread(async () =>
			{
				tPush.deleteUserPush();
				await App.Nav.PopAsync();
			});



				



		}
	}
}
