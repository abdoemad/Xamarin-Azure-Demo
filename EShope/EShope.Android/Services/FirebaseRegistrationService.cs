using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EShope.Services.Data;
using EShope.Services.Data.Imp;
using EShope.Services.Infra.Imp;
using Firebase.Iid;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace EShope.Droid.Services
{
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    //public class FirebaseRegistrationService : FirebaseInstanceIdService
    //{
    //    const string TAG = "FirebaseRegistrationService";
    //    IProductService _productService;
    //    //public FirebaseRegistrationService(IProductService productService) : base() {
    //    //    _productService = productService;
    //    //}
    //    public override void OnTokenRefresh()
    //    {
    //        var refreshedToken = FirebaseInstanceId.Instance.Token;
    //        Log.Debug(TAG, "Refreshed token: " + refreshedToken);
    //        SendRegistrationTokenToAzureNotificationHub(refreshedToken);
    //    }

    //    void SendRegistrationTokenToAzureNotificationHub(string token)
    //    {
    //        _productService = new ProductService(new APIConsumer());
    //        // Update notification hub registration
    //        Task.Run(async () =>
    //        {
    //            await AzureNotificationHubService.RegisterAsync(_productService.MobileServiceClient.GetPush(), token);
    //        });
    //    }
    //}

    // This service handles the device's registration with FCM.
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Console.WriteLine($"Token received: {refreshedToken}");
            SendRegistrationToServerAsync(refreshedToken);
        }

        async Task SendRegistrationToServerAsync(string token)
        {
            try
            {
                // Formats: https://firebase.google.com/docs/cloud-messaging/concept-options
                // The "notification" format will automatically displayed in the notification center if the 
                // app is not in the foreground.
                const string templateBodyFCM =
                    "{" +
                        "\"notification\" : {" +
                        "\"body\" : \"$(messageParam)\"," +
                          "\"title\" : \"Xamarin - EShope\"," +
                        "\"icon\" : \"myicon\" }" +
                    "}";

                var templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    {"body", templateBodyFCM}
                };

                var api = new APIConsumer();
                var client = new MobileServiceClient(api.DefaultEndPoint);
                var push = client.GetPush();

                await push.RegisterAsync(token, templates);

                // Push object contains installation ID afterwards.
                Console.WriteLine(push.InstallationId.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
    }
}