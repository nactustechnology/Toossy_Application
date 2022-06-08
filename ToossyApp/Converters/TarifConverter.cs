using ToossyApp.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class TarifConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                string tarif;

                if ((double)value != 0)
                {
                    tarif = AppResources.ItineraryDetail_Price + " : " + String.Format("{0:0.00}", value);
                }
                else
                {
                    tarif = "-";
                }

                return tarif;
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
