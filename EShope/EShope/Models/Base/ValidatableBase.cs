using EShope.Common;
using EShope.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EShope.Models.Base
{
    public class ValidatableBase : ObserverBase
    {
        readonly Validator validator;

        //public bool IsValidationEnabled
        //{
        //    get { return validator.IsValidationEnabled; }
        //    set { validator.IsValidationEnabled = value; }
        //}

        public Validator Errors
        {
            get { return validator; }
        }

        public bool HasErrors => validator.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { validator.ErrorsChanged += value; }
            remove { validator.ErrorsChanged -= value; }
        }

        public ValidatableBase()
        {
            validator = new Validator(this);
        }

        public ReadOnlyDictionary<string, ReadOnlyCollection<string>> GetAllErrors()
        {
            return validator.GetAllErrors();
        }

        public bool ValidateProperties()
        {
            return validator.ValidateProperties();
        }

        public void SetAllErrors(IDictionary<string, ReadOnlyCollection<string>> entityErrors)
        {
            validator.SetAllErrors(entityErrors);
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);

            if (result && !string.IsNullOrWhiteSpace(propertyName))
            {
                if (validator.IsValidationEnabled)
                {
                    validator.ValidateProperty(propertyName);
                }
            }
            return result;
        }
    }
}
