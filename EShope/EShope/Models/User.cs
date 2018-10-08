using EShope.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Models
{
    public class User : ObserverBase
    {
        string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
    }
}
