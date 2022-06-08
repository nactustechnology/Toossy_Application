using System;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ToossyApp.Converters
{
    public class CCDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string && value.ToString() != null)
                {
                    string DateResized = value.ToString().Replace("/", "").TrimStart(new Char[] { '0' });

                    if (DateResized.Length > 4)
                        DateResized = value.ToString().Replace("/", "").Substring(0, 4);

                    string formattedDate = String.Format("{0:00/00}", Int16.Parse(DateResized));

                    return formattedDate;
                }
                else
                {
                    return default(string);
                }
            }
            catch (Exception ex)
            {
                return default(string);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string formattedDate = Regex.Replace(value.ToString(), "/", "");

                return formattedDate;
            }

            if (value == null)
                return default(string);

            return value.ToString();
        }
    }
}
