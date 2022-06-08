using ToossyApp.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class TypeParcoursConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            { 
                if (value is int)
                {
                    var _converterDictionary = TypeParcoursDictionary.getDictionary();

                    return _converterDictionary[value.ToString()];
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
