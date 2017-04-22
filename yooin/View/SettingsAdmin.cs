using System;
using Xamarin.Forms;
namespace yooin
{
	public class SettingsAdmin : ContentPage
	{
		Entry apiurlBase = ViewFactory.createEntry("ApiUrlBase", false);
		Entry webPageUrlBase = ViewFactory.createEntry("WebUrlBase", false);
		Entry Push = ViewFactory.createEntry("PushId", false);

		BaseRelativeLayout objContent = new BaseRelativeLayout();

		public SettingsAdmin()
		{
			NavigationPage.SetHasNavigationBar(this, true);
			initialiceView();
			setViewLocation();
		}
		private void initialiceView()
		{

			//tap.Tapped += Tap_Tapped;
			//imgSettings.GestureRecognizers.Add(tap);
		}
		private void setViewLocation()
		{


			objContent.setCustomView(apiurlBase, .1, .1, .9, .13);
			objContent.setCustomViewRelative(apiurlBase, webPageUrlBase, -1, 1, .9, .1);

			Content = objContent.Content;
			Content.BackgroundColor = CustomColor.PureWithe;

		}
		protected override void OnAppearing()
		{
			var objUrl = App.Database.isLinkInDataBase();

			if (objUrl != null)
			{
				webPageUrlBase.Text = objUrl.baseWebUrl;
				apiurlBase.Text = objUrl.baseWebApi;

			}
			base.OnAppearing();
		}

		private bool savedUrl()
		{
			if (apiurlBase.Text != "" && webPageUrlBase.Text!="")
			{
				return (
					App.Database.insertLink(new tLinks
					{
						 baseWebUrl = webPageUrlBase.Text
					     ,baseWebApi = apiurlBase.Text                           
					})
				);
			}
			else {
				return false;

			}

		}
		protected override void OnDisappearing()
		{

			if (savedUrl())
			{
				base.OnDisappearing();
			}

		}

		protected override bool OnBackButtonPressed()
		{
			if (savedUrl())
			{
				return base.OnBackButtonPressed();

			}
			else {
				return false;
			}
		}
	}

}
