using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class NoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            culture = CultureInfo.InvariantCulture;

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

            string imageName = "star_1";

            try
            {
                if (value is double)
                {
                    var rating = (double)value;

                    if (rating <= 1)
                        imageName = "star_1";

                    if (rating >= 5)
                        imageName = "stars_5";

                    double decimalPart = rating % 1.0;

                    if (decimalPart< 0.5)
                    {
                        imageName = "stars_" + ((int)rating).ToString();
                    }
                    else
                    {
                        imageName = "stars_" + ((int)rating + 1).ToString();
                    }
                }

                imageName += ".png";

                return ImageSource.FromFile(imageFolder + imageName);
            }
            catch
            {
                return ImageSource.FromFile("stars_3.png");
            }

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
