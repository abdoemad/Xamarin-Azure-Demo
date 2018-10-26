using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Device
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event EventHandler<bool> ConnectivityChanged;
    }
}
