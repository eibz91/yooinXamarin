using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace yooin
{
	public class SignUp : ContentPage
	{
		CustomEntry entName = ViewFactory.CreateCustomEntry("Nombre", false,ViewFactory.KeyboardType.text);
		CustomEntry entLastName = ViewFactory.CreateCustomEntry("Apellido Paterno", false,ViewFactory.KeyboardType.text);
		CustomEntry entLastName2 = ViewFactory.CreateCustomEntry("Apellido Materno", false,ViewFactory.KeyboardType.text);
		CustomEntry entEmail = ViewFactory.CreateCustomEntry("Email", false,ViewFactory.KeyboardType.text);
		CustomEntry entPhone = ViewFactory.CreateCustomEntry("Telefono", false,ViewFactory.KeyboardType.phone);
		CustomEntry entPassword = ViewFactory.CreateCustomEntry("Contraseña", true,ViewFactory.KeyboardType.password);
		CustomEntry entPassword2 = ViewFactory.CreateCustomEntry("Repetir Contraseña", true,ViewFactory.KeyboardType.password);

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
		private void markField(CustomEntry objEntry) {
			//objEntry.BackgroundColor = Color.Red;
			objEntry.Validate();
		}
		private bool checkText(CustomEntry objEnt) {
			objEnt.getTextData();
			string data = objEnt.Text != null ? objEnt.Text.Trim() : "";
			if (data.Length != 0)
			{
				return true;

			}
			else {
				markField(objEnt);
				return false;
			}
		
		
		}
		private bool validateEmail(CustomEntry objMail) {

			try
			{
				string[] x = objMail.Text.Split('@');
				if (x.Length != 2)
				{
                     markField(objMail);
					return false;
				}
				else {
                       

					return true;
				}



			}
			catch (Exception ex) {
                markField(objMail);
				return false;
			}


		
		}
		private bool validatePassword(CustomEntry pwd, CustomEntry pwd2) {
			try
			{
				if (pwd.Text.Length >= 8 && pwd2.Text == pwd.Text)
				{
					return true;
				}
				else {
					
					markField(pwd);
                    markField(pwd2);
					return false;
				}
			}
			catch {
                    markField(pwd);

					markField(pwd2);
				return false;
			
			}

		
		}
		private bool validate() {
			bool response = true;


				response = response & checkText(entName);
				response = response & checkText(entLastName);
				response = response & checkText(entLastName2);
				response = response & checkText(entEmail);
				response = response & checkText(entPhone);
				response = response & checkText(entPassword);
				response = response & checkText(entPassword2);
				response = response & validateEmail(entEmail);
				response = response & validatePassword(entPassword,entPassword2);
				return response;








			return response;
		
		}
		private async void BtnLogin_Clicked(object sender, EventArgs e)
		{

			if (validate())
			{
				
				LoginView.fistAparence = true;
				await this.Navigation.PopAsync();
			}
			else
			{
await DisplayAlert("Mensaje", "Error al dar de alta el usuario", "ok");
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
