using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EShope.Droid.Services;
using EShope.Services.Device;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceOrientation))]

namespace EShope.Droid.Services
{
    public class DeviceOrientation : IDeviceOrientation
    {
        public DeviceOrientation() { }

        public static void Init() { }

        public DeviceOrientations GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
        }

        public static void NotifyOrientationChange(global::Android.Content.Res.Configuration newConfig)
        {
            bool isLandscape = newConfig.Orientation == global::Android.Content.Res.Orientation.Landscape;

            MessagingCenter.Send<object>(isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait, "DeviceOrientationChanged");
        }
    }
}