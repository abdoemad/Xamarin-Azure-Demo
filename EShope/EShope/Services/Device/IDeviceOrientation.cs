using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Device
{
    public enum DeviceOrientations
    {
        Undefined,
        Landscape,
        Portrait
    }
    public interface IDeviceOrientation
    {
        DeviceOrientations GetOrientation();
    }
}
