using Calculator.Shared.Models;
using Calculator.Shared.ViewModels;
using Calculator.Shared.Views.Base;
using Newtonsoft.Json;
using System.Text;

namespace Calculator.Shared.Views;

public partial class Home : BaseView
{
    private string _deviceToken;
    public Home()
    {
        InitializeComponent();
       // PushNotification();
    }

    private async void PushNotification()
    {
        var androidNotificationObject = new Dictionary<string, string>();
        androidNotificationObject.Add("NavigationID", "2");

        var iosNotificationObject = new Dictionary<string, object>();
        iosNotificationObject.Add("NavigationID", "2");

        var pushNotificationRequest = new PushNotificationRequest
        {
            notification = new NotificationMessageBody
            {
                title = "Notification Title",
                body = "Notification body"
            },
            data = androidNotificationObject,
            registration_ids = new List<string> { _deviceToken }
        };

        string url = "https://fcm.googleapis.com/fcm/send";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + "JqZh0-u1pKrBikmZ4WlZ3eiav53hv_joDvpX4uHJ9VM");

            string serializeRequest = JsonConvert.SerializeObject(pushNotificationRequest);
            var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));
            response.StatusCode = System.Net.HttpStatusCode.OK;
            await DisplayAlert("Notification sent", "notification sent", "OK");
        }
    }
}