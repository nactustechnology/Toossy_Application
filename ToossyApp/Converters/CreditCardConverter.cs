using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class CreditCardConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string && value.ToString() != null)
                {
                    string NumberResized = value.ToString().TrimStart(new Char[] { '0' });

                    if (NumberResized.Length > 16)
                        NumberResized = value.ToString().Substring(0, 16);

                    string formattedNumber = String.Format("{0:0000 0000 0000 0000}", UInt64.Parse(NumberResized));

                    return formattedNumber;
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
                string formattedNumber = Regex.Replace(value.ToString(), @"[^\d]", "");

                return formattedNumber;
            }

            if (value == null)
                return default(string);

            return value.ToString();
        }
    }
}
