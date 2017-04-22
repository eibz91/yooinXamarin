using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace yooin
{
	public class SignUp : ContentPage
	{

		Entry entName = ViewFactory.createEntry("Nombre", false);
		Entry entLastName = ViewFactory.createEntry("Apellido Paterno", false);
		Entry entLastName2 = ViewFactory.createEntry("Apellido Materno", false);
		Entry entEmail = ViewFactory.createEntry("Email", false);
		Entry entPhone = ViewFactory.createEntry("Telefono", false);

		Entry entPassword = ViewFactory.createEntry("Contraseña", true);
		Entry entPassword2 = ViewFactory.createEntry("Repetir Contraseña", true);

		Button btnLogin = ViewFactory.createButton("SignUp");
		BaseRelativeLayout objContent = new BaseRelativeLayout();

		public SignUp()
		{
			NavigationPage.SetHasNavigationBar(this, true);
			initialiceView();
			setViewLocation();
		}
		private void initialiceView()
		{
			btnLogin.BackgroundColor = Color.FromHex("#23B14D");
			btnLogin.TextColor = Color.White;
			btnLogin.BorderColor = Color.Transparent;
			btnLogin.BorderRadius = 3;

		}
		private void setViewLocation()
		{
			objContent.setCustomView(entName, .1, .08, .8, .1);
			objContent.setCustomViewRelative(entName, entLastName, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entLastName, entLastName2, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entLastName2, entEmail, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entEmail, entPhone, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entPhone, entPassword, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entPassword, entPassword2, -1, .08, .8, .1);
			objContent.setCustomViewRelative(entPassword2, btnLogin, -1, .08, .8, .1);
			btnLogin.Clicked += BtnLogin_Clicked;
			Content = objContent.Content;
			Content.BackgroundColor = CustomColor.PureWithe;
		}

		private async void BtnLogin_Clicked(object sender, EventArgs e)
		{

			if (!await sigUp())
			{
				await DisplayAlert("Mensaje", "Error al dar de alta el usuario", "ok");
			}
			else
			{
				LoginView.fistAparence = true;
				await this.Navigation.PopAsync();
			}
		}

		private async System.Threading.Tasks.Task<bool> sigUp()
		{


			if (await signUpConnection())
			{
				App.Database.insertUser(new tUser
				{
					email = entEmail.Text,
					password = entPassword.Text
				});
				return true;
			}

			return false;
		}

		private async System.Threading.Tasks.Task<bool> signUpConnection()
		{
			Connection objConnection = new Connection();
			objConnection.sUrl = tLinks.getApiUrl()+EndPoints.SigUp;
			var obj = new
			{
				sNombre = entName.Text,
				sApellidoPaterno = entLastName.Text,
				sApellidoMaterno = entLastName2.Text,
				sEmail = entEmail.Text,
				sPassword = entPassword.Text,
				bServidor = 0,
				sTelefono = entPhone.Text

			};
			objConnection.sParameter = JsonConvert.SerializeObject(obj);
			var response = await objConnection.postMethod();
			if (response != "")
			{
				try
				{
					Response objResponse = JsonConvert.DeserializeObject<Response>(response);
					if (objResponse.iErrorCode == ErrorCode.NoError) {
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
