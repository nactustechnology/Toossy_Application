using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class LangueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageFolder = "";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    imageFolder = "";
                    break;
                case Device.Android:
                    imageFolder = "Resources/drawable/";
                    break;
            }

            try
            {
                if (value is string)
                {
                    string flagName = value.ToString().ToLower().Replace("-", "_")+".png";

                    var flagSource = ImageSource.FromFile(String.Format(imageFolder+"{0}", flagName));

                    return flagSource;
                }

                return ImageSource.FromFile(imageFolder + "fr_fr.png");
            }
            catch
            {
                return ImageSource.FromFile(imageFolder + "fr_fr.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
