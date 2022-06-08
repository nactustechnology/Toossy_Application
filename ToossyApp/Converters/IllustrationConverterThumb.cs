using Xamarin.Forms;
using System.Globalization;
using System;
using ToossyApp.Resources;

namespace ToossyApp.Converters
{
    public class IllustrationConverterThumb : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageFolder = "ToossyApp.Images.";

            try
            {
                if (value is string)
                {
                    //string path = "http://nactustechnology.com/images/com_itinerary/" + value.ToString().Replace("main", "thumb");
                    string path = AppResources.ImageEndPoint_RELEASE + value.ToString().Replace("main", "thumb");
                   
                    #if DEBUG
                        path = AppResources.ImageEndPoint_DEBUG + value.ToString().Replace("main", "thumb");
                    #endif

                        var image = new UriImageSource
                    {
                        Uri = new Uri(path),
                        CachingEnabled = true,
                        CacheValidity = TimeSpan.FromHours(8)
                    };

                    if (image != null)
                        return image;


                    return ImageSource.FromResource(imageFolder + "question.png");
                }

                return ImageSource.FromResource(imageFolder + "question.png");
            }
            catch
            {
                return ImageSource.FromResource(imageFolder + "question.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
