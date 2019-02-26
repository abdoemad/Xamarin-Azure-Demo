using System;
using System.Collections.Generic;
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

namespace EShope.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseRegistrationService : FirebaseInstanceIdService
    {
        const string TAG = "FirebaseRegistrationService";
        IProductService _productService;
        //public FirebaseRegistrationService(IProductService productService) : base() {
        //    _productService = productService;
        //}
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationTokenToAzureNotificationHub(refreshedToken);
        }

        void SendRegistrationTokenToAzureNotificationHub(string token)
        {
            _productService = new ProductService(new APIConsumer());
            // Update notification hub registration
            Task.Run(async () =>
            {
                await AzureNotificationHubService.RegisterAsync(_productService.MobileServiceClient.GetPush(), token);
            });
        }
    }
}