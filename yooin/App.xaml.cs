using Xamarin.Forms;

namespace yooin
{
	public partial class App : Application
	{
		static DataBaseAction database;
		static NavigationPage nav;
		public static NavigationPage Nav
		{
			get
			{
				if (nav == null)
				{
					nav = new NavigationPage();
				}
				return nav;
			}
		}
		public static DataBaseAction Database
		{
			get
			{
				if (database == null)
				{
					database = new DataBaseAction();
				}
				return database;
			}
		}
		public App(string sVersion)
		{


			nav = new NavigationPage(new LoginView(sVersion));
			setDefaultData();
			MainPage = nav;
		}
		private void setDefaultData()
		{

			var objLink = App.Database.isLinkInDataBase();
			if (objLink == null)
			{
				App.Database.insertLink(
					new tLinks
					{
						baseWebApi = EndPoints.URLAPIBASE
						,baseWebUrl = EndPoints.URLWEBBASE
					}
				);
			}


		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
