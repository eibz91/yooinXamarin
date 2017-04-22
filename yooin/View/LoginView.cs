using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace yooin
{
	public class LoginView : ContentPage
	{
		public static bool fistAparence = true;
		Entry entUser = ViewFactory.createEntry("User", false);
		Entry entPassword = ViewFactory.createEntry("Password", true);
		Image imgSettings = ViewFactory.createImageView(ImageSource.FromFile("Setting_white.png"), Aspect.AspectFit);
		Label lblVerson = ViewFactory.createLabel("Ver. 1.00 ");
		Label lblTitleName = ViewFactory.createLabel("Yooin App");
		BoxView line = ViewFactory.createBoxView(Color.FromHex("#23B14D"), 1);
		BoxView blueHeader = ViewFactory.createBoxView(Color.FromHex("#0084BF"), 1);
		Image imgLogo = ViewFactory.createImageView(ImageSource.FromFile(".png"), Aspect.AspectFit);
		Image imgLogoHeaderNemak = ViewFactory.createImageView(ImageSource.FromFile(".png"), Aspect.AspectFit);
		Image imgpassword = ViewFactory.createImageView(ImageSource.FromFile(".png"), Aspect.AspectFit);
		Image imgUser = ViewFactory.createImageView(ImageSource.FromFile(".png"), Aspect.AspectFit);
		BoxView line2 = ViewFactory.createBoxView(Color.FromHex("#23B14D"), 1);
		BoxView blueHeader2 = ViewFactory.createBoxView(Color.FromHex("#0084BF"), 1);
		Button btnLogin = ViewFactory.createButton("Login");
		TapGestureRecognizer tap = new TapGestureRecognizer();
		BaseRelativeLayout objContent = new BaseRelativeLayout();
		int iIdCliente = 2;
		public LoginView()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			initialiceView();
			setViewLocation();
		}
		private void initialiceView()
		{
			entUser.BackgroundColor = Color.Transparent;
			entUser.Placeholder = "Username";
			entPassword.BackgroundColor = Color.Transparent;
			entPassword.Placeholder = "Password";
			btnLogin.BackgroundColor = Color.FromHex("#23B14D");
			btnLogin.TextColor = Color.White;
			btnLogin.BorderColor = Color.Transparent;
			btnLogin.BorderRadius = 3;
			lblVerson.FontSize = 15;
			lblVerson.TextColor = CustomColor.PureWithe;
			lblTitleName.TextColor = CustomColor.PureWithe;
			tap.Tapped += Tap_Tapped;
			imgSettings.GestureRecognizers.Add(tap);
		}
		private void setViewLocation()
		{
			

			//header azul
			objContent.setCustomView(blueHeader, 0, 0, 1, .18);
			//header verde
			//objContent.setCustomViewRelative(blueHeader, line, -1, .0, 1, .008);
			objContent.setCustomView(imgLogoHeaderNemak, 0, 0.03, .5, .13);
			//imagen logo
			objContent.setCustomHorizontalCenterViewSquare(imgLogo, .22, .33);
			objContent.setCustomViewRelative(blueHeader, lblTitleName, -0.45, -.21, 1, .07);

			//header azul
			objContent.setCustomView(blueHeader2, 0, .9, 1, .1);
			//header verde
			//objContent.setCustomViewRelative(blueHeader2, line2, -1, -1.002, 1, .004);
			//objContent.setCustomView(imgSettings, .9, .9, .08, .08);
			objContent.setCustomView(lblVerson, .05, .92, .6, .1);


			objContent.setLoadingScreenSize(.4, .4);

			objContent.setCustomView(entPassword, .1, .6, .8, .07);
			objContent.setCustomViewRelative(entPassword, entUser, -1, -3.1, .8, .07);
			objContent.setCustomHorizontalCenterView(btnLogin, .75, .4, .1);
			objContent.setCustomViewRelative(entPassword, imgpassword, .05, -1, .09, .09);
			objContent.setCustomViewRelative(entUser, imgUser, .06, -1, .09, .09);

			btnLogin.Clicked += BtnLogin_Clicked;
			Content = objContent.Content;

			Content.BackgroundColor = CustomColor.PureWithe;

		}
		private async void BtnLogin_Clicked(object sender, EventArgs e)
		{
			objContent.waintingScreen(true);
			if (!await doLogin())
			{
				objContent.waintingScreen(false);
				await DisplayAlert("Mensaje", "Error en el login", "ok");
			}
			else
			{
				objContent.waintingScreen(false);
				await this.Navigation.PushAsync(new MainView(entUser.Text,entPassword.Text,iIdCliente));
			}
		}
		private async System.Threading.Tasks.Task<bool> doLogin()
		{
			if (entUser.Text != "" && entPassword.Text != "") { 
				if (await loginConnection())
				{
					App.Database.insertUser(new tUser
					{
						email = entUser.Text,
						password = entPassword.Text
					});
					return true;
				}
			}
			return false;
		}
		void Tap_Tapped(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new SignUp());
		}
		protected override void OnAppearing()
		{
			tUser objUser = App.Database.isUserInDataBase();
			if (objUser != null) {
				entUser.Text = objUser.email;
				entPassword.Text = objUser.password;
			}
			if (fistAparence)
			{
				fistAparence = false;
				doLogin();
			}
		}




		private async System.Threading.Tasks.Task<bool> loginConnection()
		{
			Connection objConnection = new Connection();
			objConnection.sUrl = tLinks.getApiUrl() + EndPoints.Login;
			var obj = new
			{
				sEmail = entUser.Text,
				sPassword = entPassword.Text
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
						iIdCliente = (int)algo["iIdCLiente"];
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
