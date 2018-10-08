using EShope.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EShope.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            User = new User() { UserName = "Test" };
        }
        User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        
    }
}
