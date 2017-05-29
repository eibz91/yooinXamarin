using System;
using Android.App;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Webkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using yooin;
using yooin.Droid;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace yooin.Droid
{
	public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
	{

		const string JavaScriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
	    const string JavaScriptFunction2 = "function setScrollViewFocus(data){jsBridge.invokeAction2(data);}";
		const string JavaScriptFunction3 = "function emptyScrollViewFocus(data){jsBridge.invokeAction3(data);}";


		IValueCallback mUploadMessage;
		private static int FILECHOOSER_RESULTCODE = 1;
		protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
		{
			base.OnElementChanged(e);

			Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
			if (Control == null)
			{
				var webView = new Android.Webkit.WebView(Context);
				webView.Settings.JavaScriptEnabled = true;
				SetNativeControl(webView);
				webView.SetWebViewClient(new Android.Webkit.WebViewClient());
				var chrome = new FileChooserWebChromeClient();
				webView.Settings.JavaScriptEnabled = true;
				webView.SetWebChromeClient(chrome);
			}
			if (e.OldElement != null)
			{
				Control.RemoveJavascriptInterface("jsBridge");
				var hybridWebView = e.OldElement as HybridWebView;

				hybridWebView.Cleanup();

			}
			if (e.NewElement != null)
			{

				CookieManager cm = CookieManager.Instance;
				cm.RemoveAllCookie();
				cm.RemoveSessionCookie();

				Control.ClearCache(true);
				Control.ClearHistory();
				Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
				Control.Settings.AllowFileAccess = true;
				Control.Settings.AllowContentAccess = true;
				Control.Settings.AllowFileAccessFromFileURLs = true;
				Control.Settings.AllowUniversalAccessFromFileURLs = true;
				Control.Settings.DomStorageEnabled = true;
				Control.Settings.JavaScriptEnabled = true;
				Control.Settings.SetSupportZoom(true);
				Control.Settings.SetGeolocationEnabled(true);
				Control.LoadUrl(Element.Uri);
				InjectJS(JavaScriptFunction);
                InjectJS(JavaScriptFunction2);
                 InjectJS(JavaScriptFunction3);

			}
		}
		void InjectJS(string script)
		{
			if (Control != null)
			{
				Control.LoadUrl(string.Format("javascript: {0}", script));
			}
		}
		protected void OnActivityResult(int requestCode, Result resultCode, Intent intent)
		{
			if (requestCode == FILECHOOSER_RESULTCODE)
			{
				if (null == mUploadMessage)
					return;
				Java.Lang.Object result = intent == null || resultCode != Result.Ok
					? null
					: intent.Data;
				mUploadMessage.OnReceiveValue(result);
				mUploadMessage = null;
			}
		}

		public override void SetOnKeyListener(Android.Views.View.IOnKeyListener l)
		{
			base.SetOnKeyListener(l);
		}

		public void scrollHope(bool empty)
		{

			if (empty)
			{
				this.ScrollY = 0;
			}
			else 
			{
				int space = 0;
				space = (int)(Height*(.45));
				this.ScrollY = space;
			}


		}
	}
	public class FileChooserWebChromeClient : WebChromeClient
	{
		Action<IValueCallback, Java.Lang.String, Java.Lang.String> callback;
		IValueCallback mUploadMessage;
		private static int FILECHOOSER_RESULTCODE = 1;

		public FileChooserWebChromeClient() {
			System.Diagnostics.Debug.WriteLine("1");

		}

		public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
		{
			//base.OnGeolocationPermissionsShowPrompt(origin, callback);
			callback.Invoke(origin, true, false);
		}


		//For Android 4.1
		[Java.Interop.Export]
		public void openFileChooser(IValueCallback uploadMsg, Java.Lang.String acceptType, Java.Lang.String capture)
		{

			try
			{
				var appActivity = Xamarin.Forms.Forms.Context as MainActivity;
				mUploadMessage = uploadMsg;
				Intent i = new Intent(Intent.ActionGetContent);
				i.AddCategory(Intent.CategoryOpenable);
				i.SetType("image/*");
				appActivity.StartActivity2(Intent.CreateChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE,OnActivityResult);

			
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("3 " + ex.Message);

			}

		}


		[Android.Runtime.Register("onShowFileChooser", "(Landroid/webkit/WebView;Landroid/webkit/ValueCallback;Landroid/webkit/WebChromeClient$FileChooserParams;)Z", "GetOnShowFileChooser_Landroid_webkit_WebView_Landroid_webkit_ValueCallback_Landroid_webkit_WebChromeClient_FileChooserParams_Handler")]
		public override bool OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
		{
			try
			{
				var appActivity = Xamarin.Forms.Forms.Context as MainActivity;
				mUploadMessage = filePathCallback;
				Intent chooserIntent = fileChooserParams.CreateIntent();
				appActivity.StartActivity(chooserIntent);
				//return base.OnShowFileChooser (webView, filePathCallback, fileChooserParams);
				return true;

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("4 " + ex.Message);
				return true;
			}

		}

		private void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (data != null)
			{
				if (requestCode == FILECHOOSER_RESULTCODE)
				{
					if (null == mUploadMessage || data == null)
						return;
					Java.Lang.Object result = data == null || resultCode != Result.Ok
						? null
						: data.Data;
					mUploadMessage.OnReceiveValue(result);
					mUploadMessage = null;
				}
			}
		}


	

	}
}
