using ToossyApp.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class IsSelectedConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.Equals(true))
            {
                return AppResources.ItineraryList_Paye;
            }
            else
            {
                return AppResources.ItineraryList_Payant;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsSelectedConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(true))
            {
                return Color.DarkGreen;
            }
            else
            {
                return Color.DarkBlue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
