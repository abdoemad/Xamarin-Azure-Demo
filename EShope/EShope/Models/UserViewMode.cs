using EShope.Common;
using EShope.Models.Base;
using EShope.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShope.Models
{
    public class UserViewMode : ValidatableBase
    {
        string _userName;
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Login_UserName_EmptyValidation", ErrorMessageResourceType = typeof(ErrorMessagesResources))]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessageResourceName = "Login_UserName_SpaceSpecialCharValidation", ErrorMessageResourceType = typeof(ErrorMessagesResources))]
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public Guid Id { get; set; }
        public bool IsOnlineAuthenticate => Id != Guid.Empty;
    }
}
