using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToossyApp.Converters
{
    public class DescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = new HtmlWebViewSource();

            try
            {
                string text;

                if (value is string)
                {
                    text = "<html>" +
                            "<body  style=\"text-align: justify; font-size: 14; \">" +
                            String.Format("<p>{0}</p>", value.ToString().Replace("\r\n","<br/>")) +
                            "</body>" +
                            "</html>";
                    //return source;
                    //return text;

                }
                else
                {
                    text = "<html><body  style=\"text-align: justify;\">" +
                        value +
                        "</body></html>";
                }

                source.Html = text;

                return source;
            }
            catch(Exception ex)
            {
                source.Html = "<html><body  style=\"text-align: justify;\"></body></html>";

                return source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DescriptionHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string text;

                if (value is string)
                {
                    text = "<html>" +
                            "<body  style=\"text-align: justify;\">" +
                            String.Format("<p>{0}</p>", value.ToString().Replace("\r\n", "<br/>")) +
                            "</body>" +
                            "</html>";
                }
                else
                {
                    text = "<html><body  style=\"text-align: justify;\">" +
                        value +
                        "</body></html>";
                }


                return text;
            }
            catch (Exception ex)
            {
                return "<html><body  style=\"text-align: justify;\"></body></html>";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
