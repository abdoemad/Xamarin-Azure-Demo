using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EShope.Converters
{
    public class CachedImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageURL = value as string;
            if (imageURL is null)
            {
                return new Image
                {
                    Source = ImageSource.FromResource("EShope.Resources.Images.cart_image.png")
                };
            }

            return new UriImageSource
            {
                Uri = new Uri(value as string),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
