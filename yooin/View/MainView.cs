using System;
using Xamarin.Forms;

namespace yooin
{
	public class MainView : ContentPage
	{

		HybridWebView web;
		static BaseRelativeLayout objContent;
		public MainView(string email, string password,int iIdCliente)
		{
			NavigationPage.SetHasNavigationBar(this, false);
			objContent = new BaseRelativeLayout();
			web = new HybridWebView();
			web.Uri = tLinks.getWebUr()+string.Format(EndPoints.LOGINWEB,email,password);
 			tPush.saveCloud(iIdCliente);
			objContent.setCustomView(web, 0, 0, 1, 1);
			Content = objContent.Content;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
		}


		public static void stopLoading() {

		}


	}
}
