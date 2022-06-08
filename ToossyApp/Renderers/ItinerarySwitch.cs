using Xamarin.Forms;

namespace ToossyApp.Renderers
{
    public class ItinerarySwitch : Switch
    {
        public static readonly BindableProperty ClefProperty = BindableProperty.Create("Clef", typeof(int), typeof(ItinerarySwitch), 0);

        public int Clef
        {
            get { return (int)GetValue(ClefProperty); }
            set { SetValue(ClefProperty, value); }
        }
    }
}
