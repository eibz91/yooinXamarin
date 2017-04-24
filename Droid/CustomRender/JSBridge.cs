using System;
using Android.Webkit;
using Java.Interop;
using Xamarin.Forms;

namespace yooin.Droid
{
	public class JSBridge : Java.Lang.Object
	{
		readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;
		HybridWebViewRenderer obj;


		public JSBridge(HybridWebViewRenderer hybridRenderer)
		{
			hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
 			obj = hybridRenderer;
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

		[JavascriptInterface]
		[Export("invokeAction2")]
		public  void InvokeAction(bool scroll)
		{
			obj.scrollHope(false);
		}
		[JavascriptInterface]
		[Export("invokeAction3")]
		public  void InvokeAction2(bool scroll)
		{
			obj.scrollHope(true);
		}
	}
}
