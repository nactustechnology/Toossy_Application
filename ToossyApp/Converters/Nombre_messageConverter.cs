﻿using ToossyApp.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class Nombre_messageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return value.ToString() + " " + AppResources.ItineraryDetail_Step;
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
