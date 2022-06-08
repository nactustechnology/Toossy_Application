using ToossyApp.Models;
using Xamarin.Forms;

namespace ToossyApp.Renderers
{
    public class ItineraryListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ItineraryEntry)item).Payant == 0 ? ValidTemplate : InvalidTemplate;
        }
    }
}
