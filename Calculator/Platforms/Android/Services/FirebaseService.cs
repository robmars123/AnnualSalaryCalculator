﻿using Android.App;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Platforms.Android.Services
{
    [Service(Exported=true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT"})]
    public class FirebaseService : FirebaseMessagingService
    {
        public FirebaseService()
        {
            
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            if (Preferences.ContainsKey("DeviceToken"))
            {
                Preferences.Remove("DeviceToken");
            }

            Preferences.Set("DeviceToken", token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            var notification = message.GetNotification();
            SendNotification(notification.Body, notification.Title, message.Data);
        }

        private void SendNotification(string messageBody, string title, IDictionary<string, string> data)
        {
            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.Channel_Id)
                .SetContentTitle(title)
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetContentText(messageBody)
                .SetChannelId(MainActivity.Channel_Id)
                .SetPriority(2);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NotificationId, notificationBuilder.Build());
        }
    }
}
