using System;
using Android.App;
using Android.Content;
using Android.Media;
using Gcm.Client;
using Java.Lang;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace yooin.Droid
{
	[Service]

	public class GcmService : GcmServiceBase
	{
		public static string RegistrationToken { get; private set; }


		public GcmService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

		protected override void OnError(Context context, string errorId)
		{
			
		}

		protected override void OnMessage(Context context, Intent intent)
		{

			string msg = string.Empty;
			if (intent != null && intent.Extras != null)
			{
				foreach (var key in intent.Extras.KeySet()) { 
					if (key.ToString() == "msg")
					{
						msg = intent.Extras.Get(key).ToString();
					}
				}
			}

			// Retrieve the message
			var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
			var edit = prefs.Edit();
			edit.PutString("last_msg", msg);
			edit.Commit();

			if (!string.IsNullOrEmpty(msg))
			{
				CreateNotification("Yooin", msg);
			}
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			tPush.saveLocal(registrationId,1);
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
			
		}


		void CreateNotification(string title, string desc)
		{
			var intent = new Intent(this, typeof(MainActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			intent.PutExtra("id", "holo");
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);
			var notificationBuilder = new Notification.Builder(this)
				.SetContentTitle("Yooin")
				.SetContentText(desc)
			    .SetAutoCancel(true)
			                                          .SetNumber(2)
				.SetContentIntent(pendingIntent)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm))
			                                          .SetSmallIcon(Resource.Drawable.abc_btn_check_material)
				.SetDefaults(NotificationDefaults.Vibrate);


			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(0, notificationBuilder.Build());
    	
		}
	}
}
