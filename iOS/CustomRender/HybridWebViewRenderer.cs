using System;
using Foundation;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using yooin;
using yooin.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace yooin.iOS
{
	public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
	{

		const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
		WKUserContentController userController;


		protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
		{
			
			base.OnElementChanged(e);
			if (Control == null)
			{
				
				userController = new WKUserContentController();
				var config = new WKWebViewConfiguration { UserContentController = userController };
				var webView = new WKWebView(Frame, config);

				var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
				userController.AddUserScript(script);
				userController.AddScriptMessageHandler(this, "invokeAction");
				SetNativeControl(webView);
				if (e.OldElement != null)
				{
					userController.RemoveAllUserScripts();
					userController.RemoveScriptMessageHandler("invokeAction");
					var hybridWebView = e.OldElement as HybridWebView;
					hybridWebView.Cleanup();
				}
				if (e.NewElement != null)
				{
					var a = this.Control.Url;

					NSMutableUrlRequest sendData = new NSMutableUrlRequest(new NSUrl(Element.Uri));
					Control.LoadRequest(sendData);

				}
			}
		}



		public async void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
		{

			var jsMessage = message.Body.ToString();
			if (jsMessage != "")
			{
			}
			else {
				
					userController.RemoveAllUserScripts();
					userController.RemoveScriptMessageHandler("invokeAction");
					tPush.deleteUserPush();
					await App.Nav.PopAsync();


			}




		}


	}
}
