using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace EShope.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseNotificationService : FirebaseMessagingService
    {
        const string TAG = "FirebaseNotificationService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                Log.Debug(TAG, "From: " + message.From);

                // Pull message body out of the template
                var messageBody = string.Empty;
                if (message.Data.ContainsKey("message"))
                    messageBody = message.Data["message"];
                else
                {
                    var notification = message.GetNotification();
                    if (notification != null)
                        messageBody = notification.Body;
                }
                if (string.IsNullOrWhiteSpace(messageBody))
                    return;

                Log.Debug(TAG, "Notification message body: " + messageBody);
                SendNotification(messageBody);
            }
            catch (Exception ex)
            {
            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this)
                //.SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                //.SetSmallIcon(Resource.Drawable.cast_ic_notification_0)
                .SetSmallIcon(Resource.Drawable.cast_ic_notification_forward)
                .SetContentTitle("Eshope App - New Order")
                .SetContentText(messageBody)
                .SetContentIntent(pendingIntent)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }


    //// This service is used if app is in the foreground and a message is received.
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    //public class MyFirebaseMessagingService : FirebaseMessagingService
    //{
    //    public override void OnMessageReceived(RemoteMessage message)
    //    {
    //        base.OnMessageReceived(message);

    //        Console.WriteLine("Received: " + message);

    //        // Android supports different message payloads. To use the code below it must be something like this (you can paste this into Azure test send window):
    //        // {
    //        //   "notification" : {
    //        //      "body" : "The body",
    //        //                 "title" : "The title",
    //        //                 "icon" : "myicon
    //        //   }
    //        // }
    //        try
    //        {
    //            var msg = message.GetNotification().Body;
    //            MessagingCenter.Send<object, string>(this, XamUNotif.App.NotificationReceivedKey, msg);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("Error extracting message: " + ex);
    //        }
    //    }
    //}
}