using EShope.Helpers;
using EShope.UIExtensions.Effects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EShope.UIExtensions
{
    public static class Validation
    {
        public static readonly BindableProperty ErrorsProperty =
            BindableProperty.CreateAttached(
                "Errors",
                typeof(ReadOnlyCollection<string>),
                typeof(Validation),
                Validator.EmptyErrorsCollection,
                propertyChanged: OnPropertyErrorsChanged);

        public static ReadOnlyCollection<string> GetErrors(BindableObject element)
        {
            return (ReadOnlyCollection<string>)element.GetValue(ErrorsProperty);
        }

        public static void SetErrors(BindableObject element, ReadOnlyCollection<string> value)
        {
            element.SetValue(ErrorsProperty, value);
        }

        static void OnPropertyErrorsChanged(BindableObject element, object oldValue, object newValue)
        {
            var view = element as View;
            if (view == null | oldValue == null || newValue == null)
            {
                return;
            }

            var propertyErrors = (ReadOnlyCollection<string>)newValue;
            if (propertyErrors.Any())
            {
                view.Effects.Add(new BorderEffect());
            }
            else
            {
                var effectsToRemove = view.Effects.Where(e => e is BorderEffect).ToList();
                if (effectsToRemove != null && effectsToRemove.Count > 0)
                {
                    effectsToRemove.ForEach(effect => { view.Effects.Remove(effect); });
                }
            }
        }
    }
}
