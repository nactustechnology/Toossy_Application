using System;
using System.Globalization;
using ToossyApp.Models;
using Xamarin.Forms;
using ToossyApp.Resources;

namespace ToossyApp.Converters
{
    public class ThemeConverter2 : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is int)
                {
                    var _converterDictionary = ThemeDictionary.getDictionary();

                    return AppResources.ItineraryDetail_Theme + " : " +_converterDictionary[value.ToString()];
                }

                return value.ToString();
            }
            catch
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
