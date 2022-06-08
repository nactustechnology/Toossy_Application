using ToossyApp.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class GratuitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                string conditionAcces;

                if ((int)value == 0)
                {
                    conditionAcces = AppResources.ItineraryDetail_Free;
                }
                else
                {
                    conditionAcces = AppResources.ItineraryDetail_Chargeable;
                }

                return conditionAcces;
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
