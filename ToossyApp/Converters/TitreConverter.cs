using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class TitreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result;

            try
            {
                result = System.Net.WebUtility.HtmlDecode(value.ToString());
            }
            catch
            {
                result = value.ToString();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
