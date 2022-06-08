using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class CvcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string && value.ToString() != null)
                {
                    string CvcResized = value.ToString().Replace("/", "").TrimStart(new Char[] {'0'});

                    if (CvcResized.Length > 4)
                        CvcResized = value.ToString().Replace("/", "").Substring(0, 4);


                    string formattedCvc = String.Format("{0:000}", UInt64.Parse(CvcResized));

                    return formattedCvc;
                }
                else
                {
                    return default(string);
                }
            }
            catch(Exception ex)
            {
                return default(string);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return default(string);

            return value.ToString();
        }
    }
}
