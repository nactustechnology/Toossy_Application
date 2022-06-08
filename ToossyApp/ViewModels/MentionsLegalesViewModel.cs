using System.Threading.Tasks;
using ToossyApp.Services;
using Xamarin.Forms;
using ToossyApp.Resources;
using System;

namespace ToossyApp.ViewModels
{
    public class MentionsLegalesViewModel : BaseViewModel
    {
        public MentionsLegalesViewModel(INavService navService) : base(navService)
        {
        }

        public override async Task Init()
        {
        }

        public async Task goToCGU()
        {
            Device.OpenUri(new Uri(AppResources.DocumentEndPoint_CGU));
        }
    }
}
