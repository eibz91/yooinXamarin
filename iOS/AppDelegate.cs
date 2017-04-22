using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace yooin.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			var settings = UIUserNotificationSettings.GetSettingsForTypes( UIUserNotificationType.Sound |
							   UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			UIApplication.SharedApplication.RegisterForRemoteNotifications();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{

			string tokenToSave = deviceToken.ToString();
			tokenToSave = tokenToSave.Replace("<", "");
			tokenToSave = tokenToSave.Replace(">", "");
			tokenToSave = tokenToSave.Replace(" ", "");
			tPush.saveLocal(tokenToSave,0);

		}
		public override void DidReceiveRemoteNotification(
	UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

			string alert = string.Empty;
			if (aps.ContainsKey(new NSString("alert")))
				alert = (aps[new NSString("alert")] as NSString).ToString();

			// Show alert
			if (!string.IsNullOrEmpty(alert))
			{
				UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
				avAlert.Show();
			}
		}
	}

}
