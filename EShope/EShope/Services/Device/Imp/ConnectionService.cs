using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Device.Imp
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectivity _connectivity;

        public ConnectionService()
        {
            _connectivity = CrossConnectivity.Current;
            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, e.IsConnected);
        }

        public bool IsConnected => false;//#if d _connectivity.IsConnected;

        public event EventHandler<bool> ConnectivityChanged;
    }
    }
